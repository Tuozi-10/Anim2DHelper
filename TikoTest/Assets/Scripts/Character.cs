using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{  
    private float m_timer;

    public static float frameRate => 1/24f;
    
    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidBody;

    private bool canJump = true;
    private float speed = 5;
    
    
    public enum animEnCours
    {
        statique,
        walk,
        jump,
        cac
    }

    private animEnCours currentAnim = animEnCours.statique;

    public List<Sprite> animStatique => AnimationManager.instance.animStatique;
    public List<Sprite> animWalk => AnimationManager.instance.animWalk;
    public List<Sprite> animJump => AnimationManager.instance.animJump;
    public List<Sprite> animCac  => AnimationManager.instance.animCac ;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidBody      = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        UpdateAnimationManager();
    }

    public void InputManager()
    {
        if (currentAnim == animEnCours.cac)
            return;
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_spriteRenderer.flipX = Input.GetKey(KeyCode.LeftArrow);
            if(currentAnim != animEnCours.jump) PlayAnim(animEnCours.walk);

            m_rigidBody.velocity = Input.GetKey(KeyCode.RightArrow) ? new Vector2(speed,m_rigidBody.velocity.y) : new Vector2(-speed,m_rigidBody.velocity.y);
        }
        else
        {
            if (canJump)
            {
                PlayAnim(animEnCours.statique);
            }
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x,8);
            PlayAnim(animEnCours.jump);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            if (canJump)
            {
                PlayAnim(animEnCours.cac);
            }
        }


    }
    
    public void UpdateAnimationManager()
    {
        m_timer+=Time.deltaTime;

        switch (currentAnim)
        {
            case animEnCours.statique: UpdateStatique(); break;
            case animEnCours.walk:     UpdateWalk(); break;
            case animEnCours.jump:     UpdateJump(); break;
            case animEnCours.cac:      UpdateCac(); break;
        }
    }

    public void PlayAnim(animEnCours newAnim)
    {
        if (newAnim == animEnCours.walk && currentAnim == animEnCours.walk)
            return;

        if (newAnim == animEnCours.statique && currentAnim == animEnCours.statique)
            return;
        
        if (newAnim == animEnCours.jump) canJump = false;
        
        m_timer = 0;
        currentAnim = newAnim;
    }

    public void UpdateStatique()
    {
        var currentFrame =(int) ((m_timer / frameRate)%animStatique.Count);
        m_spriteRenderer.sprite = animStatique[currentFrame];
    }
    
    public void UpdateWalk()
    {
        var currentFrame =(int) ((m_timer / frameRate)%animWalk.Count);
        m_spriteRenderer.sprite = animWalk[currentFrame];
    }
    
    public void UpdateJump()
    {
        var currentFrame =(int) (Mathf.Min(m_timer / frameRate, animJump.Count -1));
        m_spriteRenderer.sprite = animJump[currentFrame];
        
        if(canJump) PlayAnim(animEnCours.statique);
    }
    
    public void UpdateCac()
    {
        var currentFrame =(int) ((m_timer / frameRate)%animCac.Count);
        m_spriteRenderer.sprite = animCac[currentFrame];
        
        if((int)(m_timer / frameRate) >= animCac.Count)
            PlayAnim(animEnCours.statique);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }
}
