using UnityEngine;
using TMPro;

public class AssignCustomMaterial : MonoBehaviour
{
    public Material customMaterial;
    public TMP_Text textMeshPro;

    void Start()
    {
        // Find the TextMeshPro component on this GameObject
        textMeshPro = GetComponent<TMP_Text>();

        // Assign the custom material to the TextMeshPro text object
        if (textMeshPro != null && customMaterial != null)
        {
            textMeshPro.material = customMaterial;
        }

        textMeshPro.color = Color.red;
    }
}
