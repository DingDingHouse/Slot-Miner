#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace MyBuilder
{
    public class WebGLBuilder
    {
        [MenuItem("Build/Build My WebGL")]
        public static void Build()
        {
            // Set up the build path
            string buildPath = "Builds/WebGL";
            CreateDirectory(buildPath);

            // Gather all enabled scenes
            string[] scenes = GetEnabledScenes();

            if (scenes.Length == 0)
            {
                Debug.LogError("No scenes found in build settings. Please ensure scenes are added to EditorBuildSettings.");
                return;
            }

            // Define build options
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = buildPath,
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            };

            // Start the build process
            string buildResult = BuildPipeline.BuildPlayer(buildPlayerOptions).ToString();
            
            if (string.IsNullOrEmpty(buildResult))
            {
                Debug.Log("WebGL Build complete!");
            }
            else
            {
                Debug.LogError("WebGL Build failed with errors: " + buildResult);
            }
        }

        private static string[] GetEnabledScenes()
        {
            // Retrieves enabled scenes from the build settings
            return EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();
        }

        private static void CreateDirectory(string path)
        {
            // Ensures the build directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
#endif
