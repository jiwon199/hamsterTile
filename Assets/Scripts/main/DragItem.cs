using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DragItem : MonoBehaviour
{
    static public DragItem instance;

    public item dragItem;

    //아이템 이미지
    [SerializeField] private Image imageItem;


    void Start(){
        instance=this;
        DragItem.instance.SetColor(0);
    }

    public void DragSetImage(Image _itemImage){
        DragItem.instance.SetColor(0.7f);
        imageItem.sprite=_itemImage.sprite;
    }

    public void SetColor(float _alpha){
        Color color = imageItem.color;
        color.a=_alpha;
        imageItem.color=color;
    }
}
