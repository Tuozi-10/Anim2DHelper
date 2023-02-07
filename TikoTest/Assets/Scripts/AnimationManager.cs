using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
 
    private Character m_character;
    
    public List<Sprite> animStatique = new List<Sprite>();
    public List<Sprite> animWalk     = new List<Sprite>();
    public List<Sprite> animJump     = new List<Sprite>();
    public List<Sprite> animCac      = new List<Sprite>();

    public static AnimationManager instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
         LoadSprites();
    }

    public void LoadSprites()
    {
        if (!Directory.Exists(@$"{Application.dataPath}\AnimStatique"))
            Directory.CreateDirectory(@$"{Application.dataPath}\AnimStatique");   
        
        if (!Directory.Exists(@$"{Application.dataPath}\AnimWalk"))
            Directory.CreateDirectory(@$"{Application.dataPath}\AnimWalk");
        
        if (!Directory.Exists(@$"{Application.dataPath}\AnimJump"))
            Directory.CreateDirectory(@$"{Application.dataPath}\AnimJump");
        
        if (!Directory.Exists(@$"{Application.dataPath}\AnimCac"))
            Directory.CreateDirectory(@$"{Application.dataPath}\AnimCac");
        
        animStatique = new List<Sprite>();
        animWalk     = new List<Sprite>();
        animJump     = new List<Sprite>();
        animCac      = new List<Sprite>();
        
        // Static
        LoadTexturesFromFolder(@$"{Application.dataPath}\AnimStatique", out List<Texture2D> texsStatique);

        foreach (var tex in texsStatique)
        { 
            animStatique.Add(Sprite.Create(tex, new Rect(0,0f, tex.width, tex.height), new Vector2(0.5f, 0f)));
        }
        
        // Walk
        LoadTexturesFromFolder(@$"{Application.dataPath}\AnimWalk", out List<Texture2D> texsWalk);

        foreach (var tex in texsWalk)
        { 
            animWalk.Add(Sprite.Create(tex, new Rect(0,0f, tex.width, tex.height), new Vector2(0.5f, 0f)));
        }
        
        // Jump
        LoadTexturesFromFolder(@$"{Application.dataPath}\AnimJump", out List<Texture2D> texsJump);
        
       foreach (var tex in texsJump)
       { 
           animJump.Add(Sprite.Create(tex, new Rect(0,0f, tex.width, tex.height), new Vector2(0.5f, 0f)));
       }
        
       // Cac
       LoadTexturesFromFolder(@$"{Application.dataPath}\AnimCac", out List<Texture2D> texsCac);
        
       foreach (var tex in texsCac)
       { 
           animCac.Add(Sprite.Create(tex, new Rect(0,0f, tex.width, tex.height), new Vector2(0.5f, 0f)));
       }
    }

    public void LoadTexturesFromFolder(string path, out List<Texture2D> texs)
    {
        texs = new List<Texture2D>();
        
        foreach (var file in Directory.GetFiles(path, "*.png"))
        {
            if (file.Contains(".meta")) 
                continue;
            
            var bytes = File.ReadAllBytes(file);
            var tex = new Texture2D(2, 2);
            tex.LoadImage(bytes);
            texs.Add(tex);
        }
    }
    
    
}
