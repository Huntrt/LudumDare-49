using UnityEditor;

public class QuickWorkspace : EditorWindow
{
	//When click on Tool > Open Code in menu item
	[MenuItem("Tools/Open Code")] static void ShowWindow()
	{
		//Open the workspace file if user click this button
		AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath
		//Search for the workspace location and open it
		("Assets/Editor/Quick Workspace/Project Workspace.code-workspace", typeof(DefaultAsset)));
	}
}

[InitializeOnLoad] class LaunchWorkspace
{
	const string launched = ""; static LaunchWorkspace()
	{
		//If haven't launch the workspace
		if(!SessionState.GetBool(launched, false))
		{
			//Has launch the workspace
			SessionState.SetBool(launched, true);
			//Open the workspace file if user click this button
			AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath
			//Search for the workspace location and open it
			("Assets/Editor/Quick Workspace/Project Workspace.code-workspace", typeof(DefaultAsset)));
		}
	}
}