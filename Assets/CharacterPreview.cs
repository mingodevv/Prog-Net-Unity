using UnityEngine;

public class SimpleModelPreview : MonoBehaviour
{
    public GameObject modelPrefab;

    private GameObject currentModel;

    void Start()
    {
        if (modelPrefab == null)
        {
            Debug.LogError("Aucun modèle assigné dans SimpleModelPreview !");
            return;
        }

        // Instancie le modèle à cet emplacement
        currentModel = Instantiate(modelPrefab, transform.position, transform.rotation, transform);

        // Mets l'échelle pour s'adapter à ton UI
        currentModel.transform.localScale = Vector3.one * 1f;
    }
}