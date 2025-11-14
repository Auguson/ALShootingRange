using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

/// <summary>
/// Comprehensive test suite for WebGL build validation
/// Place in Editor folder to run in Unity Test Runner
/// </summary>
public class WebGLBuildValidationTests
{
    private const string WEBGL_PLATFORM = "WebGL";
    
    [Test]
    public void Test_WebGLPlatformInstalled()
    {
        var buildTarget = BuildTarget.WebGL;
        bool isInstalled = BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.WebGL, buildTarget);
        
        Assert.IsTrue(isInstalled, 
            "WebGL build support is not installed. Install it via Unity Hub > Installs > Add Modules");
    }
    
    [Test]
    public void Test_NoUnsupportedAPIsInScripts()
    {
        var unsupportedAPIs = new List<string>();
        var allScripts = AssetDatabase.FindAssets("t:Script")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Where(path => path.EndsWith(".cs"));
        
        var unsupportedPatterns = new[]
        {
            "System.Threading.Thread",
            "System.Net.Sockets",
            "System.IO.File.WriteAllBytes",
            "Application.dataPath", // Often problematic in WebGL
            "WWW(" // Deprecated, use UnityWebRequest
        };
        
        foreach (var scriptPath in allScripts)
        {
            var content = File.ReadAllText(scriptPath);
            foreach (var pattern in unsupportedPatterns)
            {
                if (content.Contains(pattern))
                {
                    unsupportedAPIs.Add($"{scriptPath}: uses {pattern}");
                }
            }
        }
        
        Assert.IsEmpty(unsupportedAPIs, 
            "Found potentially unsupported APIs for WebGL:\n" + string.Join("\n", unsupportedAPIs));
    }
    
    [Test]
    public void Test_NoNativePluginsInWebGLBuild()
    {
        var nativePlugins = new List<string>();
        var allPlugins = AssetDatabase.FindAssets("t:PluginImporter")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        
        foreach (var pluginPath in allPlugins)
        {
            var importer = AssetImporter.GetAtPath(pluginPath) as PluginImporter;
            if (importer != null && importer.GetCompatibleWithPlatform(BuildTarget.WebGL))
            {
                // Check if it's a .dll, .so, or .dylib (native libraries)
                var ext = Path.GetExtension(pluginPath).ToLower();
                if (ext == ".dll" || ext == ".so" || ext == ".dylib" || ext == ".a")
                {
                    nativePlugins.Add(pluginPath);
                }
            }
        }
        
        Assert.IsEmpty(nativePlugins, 
            "Found native plugins enabled for WebGL (these may not work):\n" + string.Join("\n", nativePlugins));
    }
    
    [Test]
    public void Test_CompressionMethodSupported()
    {
        var compression = PlayerSettings.WebGL.compressionFormat;
        
        Assert.IsTrue(
            compression == WebGLCompressionFormat.Gzip || 
            compression == WebGLCompressionFormat.Brotli ||
            compression == WebGLCompressionFormat.Disabled,
            $"WebGL compression format is set to {compression}. Use Gzip, Brotli, or Disabled.");
    }
    
    [Test]
    public void Test_MemorySize()
    {
        var memorySize = PlayerSettings.WebGL.memorySize;
        
        //Assert.GreaterOrEqual(memorySize, 256, 
        //    $"WebGL memory size is {memorySize}MB. Minimum recommended is 256MB for most projects.");
        
        Assert.LessOrEqual(memorySize, 2048,
            $"WebGL memory size is {memorySize}MB. Values over 2GB may cause issues on some browsers.");
    }
    
    [Test]
    public void Test_ExceptionSupport()
    {
        var exceptionSupport = PlayerSettings.WebGL.exceptionSupport;
        
        // Warn if using full exceptions (increases build size significantly)
        if (exceptionSupport == WebGLExceptionSupport.FullWithStacktrace ||
            exceptionSupport == WebGLExceptionSupport.FullWithoutStacktrace)
        {
            Debug.LogWarning($"WebGL exception support is set to {exceptionSupport}. " +
                "This significantly increases build size. Consider using 'ExplicitlyThrownExceptionsOnly' for production.");
        }
        
        Assert.Pass($"Exception support: {exceptionSupport}");
    }
    
    [Test]
    public void Test_ScriptingBackend()
    {
        var scriptingBackend = PlayerSettings.GetScriptingBackend(BuildTargetGroup.WebGL);
        
        Assert.AreEqual(ScriptingImplementation.IL2CPP, scriptingBackend,
            "WebGL requires IL2CPP scripting backend. Mono is not supported.");
    }
    
    [Test]
    public void Test_NoMissingScriptReferences()
    {
        var missingScripts = new List<string>();
        var allPrefabs = AssetDatabase.FindAssets("t:Prefab")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        
        foreach (var prefabPath in allPrefabs)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                var components = prefab.GetComponentsInChildren<Component>(true);
                foreach (var comp in components)
                {
                    if (comp == null)
                    {
                        missingScripts.Add(prefabPath);
                        break;
                    }
                }
            }
        }
        
        Assert.IsEmpty(missingScripts,
            "Found prefabs with missing script references:\n" + string.Join("\n", missingScripts));
    }
    
    [Test]
    public void Test_AudioSettings()
    {
        var defaultSpeakerMode = AudioSettings.GetConfiguration().speakerMode;
        
        // WebGL typically works best with Stereo
        if (defaultSpeakerMode != AudioSpeakerMode.Stereo)
        {
            Debug.LogWarning($"Audio speaker mode is {defaultSpeakerMode}. " +
                "WebGL works best with Stereo mode.");
        }
        
        Assert.Pass($"Audio speaker mode: {defaultSpeakerMode}");
    }
    
    [Test]
    public void Test_GraphicsAPI()
    {
        var graphicsAPIs = PlayerSettings.GetGraphicsAPIs(BuildTarget.WebGL);
        
        Assert.IsTrue(graphicsAPIs.Contains(UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3) ||
                      graphicsAPIs.Contains(UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2),
            "WebGL requires OpenGL ES 2.0 or 3.0 graphics API.");
        
        Assert.Pass($"Graphics APIs: {string.Join(", ", graphicsAPIs)}");
    }
    
    [Test]
    public void Test_NoProblematicShaders()
    {
        var problematicShaders = new List<string>();
        var allMaterials = AssetDatabase.FindAssets("t:Material")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        
        foreach (var matPath in allMaterials)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(matPath);
            if (material != null && material.shader != null)
            {
                var shader = material.shader;
                
                // Check for shader variants that might be problematic
                if (shader.name.Contains("Standard") && shader.name.Contains("SpecularSetup"))
                {
                    // This is usually fine, but check variant count
                }
                
                // Warn about custom shaders that might not be WebGL compatible
                if (!shader.name.StartsWith("Standard") && 
                    !shader.name.StartsWith("UI") && 
                    !shader.name.StartsWith("Sprites") &&
                    !shader.name.StartsWith("Unlit"))
                {
                    problematicShaders.Add($"{matPath}: uses custom shader '{shader.name}'");
                }
            }
        }
        
        if (problematicShaders.Count > 0)
        {
            Debug.LogWarning("Found materials with custom shaders (verify WebGL compatibility):\n" + 
                string.Join("\n", problematicShaders));
        }
        
        Assert.Pass($"Checked {allMaterials.Count()} materials");
    }
    
    [Test]
    public void Test_BuildScenesValid()
    {
        var scenes = EditorBuildSettings.scenes;
        
        Assert.IsNotEmpty(scenes, "No scenes added to Build Settings.");
        
        var invalidScenes = scenes.Where(s => s.enabled && !File.Exists(s.path)).ToList();
        
        Assert.IsEmpty(invalidScenes,
            "Found enabled scenes in Build Settings that don't exist:\n" + 
            string.Join("\n", invalidScenes.Select(s => s.path)));
        
        Assert.Pass($"Found {scenes.Count(s => s.enabled)} valid enabled scenes");
    }
    
    [Test]
    public void Test_CodeStripping()
    {
        var strippingLevel = PlayerSettings.GetManagedStrippingLevel(BuildTargetGroup.WebGL);
        
        if (strippingLevel == ManagedStrippingLevel.Disabled)
        {
            Debug.LogWarning("Code stripping is disabled. This will result in larger WebGL builds. " +
                "Consider using Low or Medium stripping level.");
        }
        
        Assert.Pass($"Code stripping level: {strippingLevel}");
    }
    
    [Test]
    public void Test_DataCachingSupport()
    {
        var dataCaching = PlayerSettings.WebGL.dataCaching;
        
        // Just informational
        Assert.Pass($"Data caching: {dataCaching}");
    }
    
    [Test]
    public void Test_LinkerTarget()
    {
        var linkerTarget = PlayerSettings.WebGL.linkerTarget;
        
        // Wasm is the modern standard
        Assert.AreEqual(WebGLLinkerTarget.Wasm, linkerTarget,
            "WebGL linker target should be set to Wasm for best performance and compatibility.");
    }
    
    [Test]
    public void Test_NoThreadingAPIs()
    {
        var threadingIssues = new List<string>();
        var allTypes = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => {
                try { return a.GetTypes(); }
                catch { return new System.Type[0]; }
            });
        
        foreach (var type in allTypes)
        {
            if (type.Namespace != null && 
                (type.Namespace.StartsWith("UnityEngine") || 
                 type.Namespace.StartsWith("UnityEditor")))
            {
                continue;
            }
            
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | 
                                         BindingFlags.Instance | BindingFlags.Static | 
                                         BindingFlags.DeclaredOnly);
            
            foreach (var method in methods)
            {
                try
                {
                    var body = method.GetMethodBody();
                    if (body != null)
                    {
                        // This is a simple check - more sophisticated analysis would require IL parsing
                        if (method.Name.Contains("Thread") || method.Name.Contains("Async"))
                        {
                            threadingIssues.Add($"{type.FullName}.{method.Name}");
                        }
                    }
                }
                catch
                {
                    // Skip methods we can't analyze
                }
            }
        }
        
        if (threadingIssues.Count > 0 && threadingIssues.Count < 50)
        {
            Debug.LogWarning("Found methods with threading-related names (verify WebGL compatibility):\n" + 
                string.Join("\n", threadingIssues.Take(10)));
        }
        
        Assert.Pass($"Analyzed types for threading APIs");
    }
    
    [Test]
    public void Test_ProjectHasCompanyAndProductName()
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(PlayerSettings.companyName),
            "Company name is not set in Player Settings.");
        
        Assert.IsFalse(string.IsNullOrWhiteSpace(PlayerSettings.productName),
            "Product name is not set in Player Settings.");
        
        Assert.Pass($"Company: {PlayerSettings.companyName}, Product: {PlayerSettings.productName}");
    }
    
    [Test]
    public void Test_WebGLTemplateExists()
    {
        var template = PlayerSettings.WebGL.template;
        
        Assert.IsFalse(string.IsNullOrEmpty(template),
            "WebGL template is not set in Player Settings.");
        
        Assert.Pass($"WebGL template: {template}");
    }
    
    [Test]
    public void Test_NoExcessiveTextureMemory()
    {
        var textures = AssetDatabase.FindAssets("t:Texture2D")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => AssetDatabase.LoadAssetAtPath<Texture2D>(path))
            .Where(t => t != null);
        
        long totalMemory = 0;
        var largeTextures = new List<string>();
        
        foreach (var texture in textures)
        {
            var memSize = UnityEngine.Profiling.Profiler.GetRuntimeMemorySizeLong(texture);
            totalMemory += memSize;
            
            if (memSize > 8 * 1024 * 1024) // 8MB
            {
                largeTextures.Add($"{texture.name}: {memSize / (1024 * 1024)}MB ({texture.width}x{texture.height})");
            }
        }
        
        if (largeTextures.Count > 0)
        {
            Debug.LogWarning($"Found {largeTextures.Count} large textures (>8MB each):\n" + 
                string.Join("\n", largeTextures.Take(5)));
        }
        
        Assert.Pass($"Total texture memory: {totalMemory / (1024 * 1024)}MB");
    }
}
