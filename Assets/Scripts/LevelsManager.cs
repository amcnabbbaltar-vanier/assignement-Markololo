// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LevelsManager : MonoBehaviour
// {
//     private Rigidbody rb;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         Debug.Log("Collision detected with: " + collision.gameObject.name);
//         if (collision.gameObject.CompareTag("DeathPlane"))
//         {
//             GameManager.Instance.ResetGame(); 
//         }

//         if (collision.gameObject.CompareTag("Level1_pass"))
//         {
//             SceneManager.LoadScene("Scenes/Level2Scene");
//         }
//         if (collision.gameObject.CompareTag("Level2_pass"))
//         {
//             SceneManager.LoadScene("Scenes/Level3Scene");
//         }
//         if (collision.gameObject.CompareTag("Level3_pass"))
//         {
//             SceneManager.LoadScene("Scenes/endScene");
//         }
//     }
// }
