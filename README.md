# Flappy bird with Unity

### 🚀 [https://abdoul-sudo.itch.io/flappy-bird](https://abdoul-sudo.itch.io/flappy-bird)
![Capture d’écran (25)](https://github.com/Abdoul-sudo/unity_flappyBird/assets/78481157/426fdc05-0e35-4b28-9088-98deae17f27e)
![Capture d’écran (28)](https://github.com/Abdoul-sudo/unity_flappyBird/assets/78481157/62cf57b1-36f8-4c4d-b042-de61e5c9c549)

## Steps

### 1- Game Object Visual

- Create Empty Game Object <b>Bird</b>
- Add Sprite Renderer component and add the sprite

### 2- Physics

- Add a Rigidbody2D to the <b>Bird</b>
- Add a CircleCollider and reduce the size (Game Design tips), center it
- Add a script to the <b>Bird</b>
- Add rigidbody variable - drag Rigidbody2D to the variable field
- Add flapStrength variable - change value - change gravity scale of the rigidbody

### 3- Pipe

- Create Empty Game Object <b>Pipe</b>
- Create another one TopPipe within it
- Add Sprite Renderer component to <b>TopPipe</b> and add the sprite
- Add Box Collider 2D component to <b>TopPipe</b>
- Duplicate <b>TopPipe</b> for <b>BottomPipe</b>
- Add a script to <b>Pipe</b> (to move it)
- <details>
  <summary>Add code for <b>Pipe</b> movement and adjust framerate with <code>Time.deltaTime</code></summary>

  ```csharp
  transform.position = transform.position + (Vector3.left _ moveSpeed) _ Time.deltaTime;

  ```

  <b style="color: red;">PS: </b><b>Always add <code>Time.deltaTime</code> when using transform property</b>

  </details>

### 4- Pipes spawn and prefabs

- Drag the <b>Pipe</b> gameObject to the project to create a <b>Prefab</b>
- Delete the original Pipe gameObject
- Create a <b>PipeSpawnerScript</b> and add a <code>GameObject pipe</code> variable
- Drag the pipe prefab to the pipe variable field
- <details>
  <summary><b>PipeSpawnerScript:</b>  Instanciate pipe</summary>

  ```csharp
  void spawnPipe()
  {
      float lowestPoint = transform.position.y - heightOffset;
      float highestPoint = transform.position.y + heightOffset;

      Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint)), transform.rotation);
  }

  ```

  </details>

- <details>
  <summary><b>PipeMoveScript:</b>  Delete pipe on deadzone</summary>

  ```csharp
  public float deadZone = -45;

  if (transform.position.x < deadZone)
  {
      Destroy(gameObject);
  }

  ```

  </details>

### 5- UI

- Create a Text UI (UI > Legacy > Text) and adjust it
- Create an Empty GameObject <b>Logic Manager</b> and add <b>LogicScript</b> component to it
- Add <code>ScoreText</code> variable and drag the <b>Text UI</b> to the variable field

* <b style="color: red;">PS: </b><b>We can use <code>[ContextMenu("Increase Score")]</code> above a function to execute it directly in Unity</b>

### 6- Game logic and collisions

- Edit the pipe prefab, add and empty gameObject <b>middle</b> with boxCollider2D (check <b>isTrigger</b> property)
- Adjust the boxCollider2D sizeY to fit between the top and bottom pipes
- Add <b>PipeMiddleScript</b> to <b>middle</b>
- We want to increase score when passing through <b>middle</b> so

  - Add Tag <b>Logic</b> to <b>Logic Manager</b> gameObject
  - We can't drag to variable field when using prefab, so we select the <b>Logic Manager</b> gameObject in code:
    <details>
    <summary><b>PipeMiddleScript:</b> increase score OnTriggerEnter2D</summary>

    ```csharp
    public class PipeMiddleScript : MonoBehaviour
    {
        public LogicScript logic;

        void Start()
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            logic.addScore();
        }
    }

    ```

    </details>

### 7- Game Over screen

- Inside UI canvas, create empty GameObject <b>Game Over Screen</b>
- Then create a UI Text and Button children (UI > Legacy > ...), resize and change texts
- Restart button

  - <details>
    <summary>Create a restart function in <b>LogicScript</b></summary>

    ```csharp
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    ```

    </details>

  - drag <b>LogicScript</b> to the button "click" function
  - select the restart function

- Uncheck the active checkbox of <b>Game Over Screen</b> to make it disapear by default
- <details>
    <summary>Add a Game Over function to <b>Logic Script</b> to show the <b>Game Over Screen</b></summary>

  ```csharp
  public GameObject gameOverScreen;

  public void gameOver()
  {
      gameOverScreen.SetActive(true)
  }

  ```

    </details>

- Trigger the gameOver() when the bird touches the pipe. Get the LogicScript reference and trigger the function <b>onCollisionEnter2D</b>.
  <details>
    <summary><b>BirdScript</b></summary>

  ```csharp
  public Rigidbody2D myRigidbody;
  public float flapStrength;
  void Start()
  {
      logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
  }
  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

  private void OnCollisionEnter2D(Collision2D collision)
  {
      logic.gameOver();
      birdIsAlive = false;
  }

  ```

  </details>

### 7- Build the Game

- File > Build Settings
