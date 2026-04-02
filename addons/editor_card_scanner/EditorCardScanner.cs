// EditorCardScanner.cs
#if TOOLS
using Godot;
using System.Collections.Generic;
using System.IO;

[Tool]
public partial class EditorCardScanner : EditorPlugin
{
	public override void _EnterTree()
	{
		GenerateManifest();
	}

	private void GenerateManifest()
	{
		string cardsPath = "res://resources/cards/";
		var dir = DirAccess.Open(cardsPath);
		if (dir == null) return;

		var paths = new List<string>();
		ScanDirectory(dir, cardsPath, paths);
		GD.Print(paths);
		var manifest = new CardManifest();
		manifest.CardPaths = paths.ToArray();
		
		string manifestPath = "res://resources/card_manifest.tres";
		ResourceSaver.Save(manifest, manifestPath);
		GD.Print($"Card manifest saved with {manifest.CardPaths.Length} entries.");
	}

	private void ScanDirectory(DirAccess dir, string basePath, List<string> outPaths)
	{
		dir.ListDirBegin();
		while (true)
		{
			string file = dir.GetNext();
			if (string.IsNullOrEmpty(file)) break;
			if (file == "." || file == "..") continue;

			string fullPath = basePath + file;
			if (dir.CurrentIsDir())
			{
				using var subDir = DirAccess.Open(fullPath);
				if (subDir != null)
					ScanDirectory(subDir, fullPath + "/", outPaths);
			}
			else if (file.EndsWith(".tres") || file.EndsWith(".res"))
			{
				outPaths.Add(fullPath);
			}
		}
		dir.ListDirEnd();
	}
}
#endif
