using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour {

    public string id;
    public Color color;

    private Image _image;
    private Color _baseColor;
    
    private void Awake() {
        _image = GetComponent<Image>();
        _baseColor = _image.color;
    }

    public void OnClick() {

        GameManager.Instance.OnCardClick(gameObject);
    }

    public void SwitchColor() {
        _image.color = color;
    }

    public void RollBackColor() {
        _image.color = _baseColor;
    }

}
