using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private Image imageReference;
    [SerializeField] private Image iconImageReference;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isInside;


    private void Start()
    {
        animator = GetComponent<Animator>();
        isInside = true;
    }
    public void SelectImage(int imageSelect)
    {
        imageReference.sprite = images[imageSelect].sprite;
        iconImageReference.sprite = images[imageSelect].sprite;
    }

    public void Inside()
    {
        isInside = !isInside;

        if (isInside) { animator.SetTrigger("Up"); }
        else
        {
            animator.SetTrigger("Down");
        }
    }

    public void Clear()
    {
        imageReference.sprite = null;
        iconImageReference.sprite = null;
    }


}
