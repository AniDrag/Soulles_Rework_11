using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Door
{
    public RectInt door { get; private set; }

}

public class Room
{
    public RectInt thisRoom { get; private set; }

    public Door doorUp;
    public Door doorDown;
    public Door doorLeft;
    public Door doorRight;
}
/// <summary>
/// DungeonGenerator is a MonoBehaviour class responsible for generating a dungeon layout.
/// It creates rooms of random sizes, splits them into smaller rooms, adds doors between rooms,
/// deletes random rooms, connects paths, and assigns a starting room.
/// </summary>
public class DungeonGenerator : MonoBehaviour
{
    [Header("----- Dungeon paramaters -----")]
    [SerializeField] RectInt theDungeon;
    [SerializeField] int dungeonWidth = 50;
    [SerializeField] int dungeonLenght = 50;
    [SerializeField] int dungeonHeight = 10;
    [SerializeField] int dungeonFloors = 1;
    [SerializeField] int seed;
    bool dungeonInitialized;
    bool dungeonLoaded;

    [Header("----- Room paramaters -----")]
    [SerializeField] int roomMinWidth = 5;
    [SerializeField] int roomMinLenght = 5;
    public List<RectInt> roomList = new List<RectInt>();
    bool roomLoaded;

    [Header("----- Door paramaters -----")]
    [SerializeField] int doorWidth = 2;
    [SerializeField] int doorLenght = 3;
    public List<RectInt> doorList = new List<RectInt>();
    bool doorsLoaded;

    [Header("----- Wireframe progress -----")]
    [SerializeField] bool removeRandomRooms;
    [SerializeField] bool playerRoomAssigned;
    [SerializeField] bool bossRoomAssigned;
    [SerializeField] bool pathfindingCompleted;

    [Header("----- Physical progress -----")]
    [SerializeField] bool wallsBuilt;
    [SerializeField] bool floorsBuilt;
    [SerializeField] bool doorsBuilt;
    [SerializeField] bool navMeshBuilt;
    [SerializeField] bool spawnersDistributed;
    [SerializeField] bool bossSpanerPlaced;

    [Header("----- Physical progress -----")]
    [SerializeField] float _time;

    private void Start()
    {
        // InitializeDungeon();
        Random.InitState(seed);// makes all Random functons runn a certian patern
    }
    private void Update()
    {
        //_time += Time.deltaTime; // when initialized dungeon start
    }

    [Button("Initialize room spliting", enabledMode: EButtonEnableMode.Playmode)]
    void InitializeDungeon()
    {


    }
    [Button("Initialize room spliting", enabledMode: EButtonEnableMode.Playmode)]
     void StartSplitingRooms()
    {

    }
    [Button("Initialize door distribution", enabledMode: EButtonEnableMode.Playmode)]
    void DistributeDoors()
    {

    }
    (Room, Room) SplitingRoom(Room room)
    {
        Room a,b;


        return (a, b);
    }

    void AssigningDoors(Room room)
    {

    }


    void TimeToLoadWireframe()// print time when wireframe finishes
    {
        
        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);

        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log(time);
    }
    void TimeToLoadPhysical()// print time when physical Dungeon finishes
    {
        
        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);

        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log(time);
    }
}
    
    
    
    
    
    
    
    /*
    [Header("Dungeon Settings")]
    [SerializeField] private int dungeonLength = 100; // Total size of the dungeon (width and height).
    [SerializeField] private int maxRooms = 2046; // Maximum number of rooms to generate.
    [SerializeField] private int seed = 0; // Seed for random number generation.

    [Header("Room Settings")]
    [SerializeField] private int minRoomWidth = 5; // Minimum width of a room.
    [SerializeField] private int minRoomHeight = 5; // Minimum height of a room.
    [SerializeField] private int wallThickness = 2; // Thickness of walls between rooms.

    [Header("Door Settings")]
    [SerializeField] private int doorWidth = 2; // Width of doors.
    [SerializeField] private int doorHeight = 2; // Height of doors.

    private RectInt _dungeonArea; // The area of the dungeon.
    private int _currentRoomDupIndex; // Index to track which room to split next.
    private readonly List<RectInt> _roomsArray = new(); // List of all rooms in the dungeon.
    private readonly List<RectInt> _doorsArray = new(); // List of all doors in the dungeon.

    private bool _initializedDungeon; // Flag to check if the dungeon has been initialized.
    private bool _noMoreSplitableRooms; // Flag to check if no more rooms can be split.

    private void Update()
    {
        if (!_initializedDungeon) return;

        // Stop generating rooms if the maximum number of rooms is reached or no more rooms can be split.
        if (_roomsArray.Count >= maxRooms || _noMoreSplitableRooms)
        {
            Debug.Log("Max room count reached!");
            _initializedDungeon = false;

            AssignStartingRoom(); // Assign the starting room first.
            GenerateDoors(); // Generate doors between rooms.
            DeleteRandomRooms(); // Delete random rooms to create a more interesting layout.
            ConnectPaths(); // Ensure all remaining rooms are connected.

            Debug.Log("Dungeon generation completed successfully!");
            return;
        }

        GenerateRooms(); // Continue generating rooms.
    }

    /// <summary>
    /// Initializes the dungeon by setting up the initial area and starting the room generation process.
    /// </summary>
    [Button("Initialize Dungeon", EButtonEnableMode.Playmode)]
    private void InitializeDungeon()
    {
        Debug.Log("Generating Dungeon...");
        Random.InitState(seed); // Set the random seed for reproducibility.
        _dungeonArea = new RectInt(0, 0, dungeonLength, dungeonLength); // Define the dungeon area.
        _roomsArray.Add(_dungeonArea); // Add the initial dungeon area to the rooms list.
        SplitRooms(_dungeonArea); // Start splitting the initial area into smaller rooms.
        _initializedDungeon = true; // Mark the dungeon as initialized.
    }

    /// <summary>
    /// Splits a given room into two smaller rooms, either vertically or horizontally.
    /// </summary>
    /// <param name="room">The room to split.</param>
    /// <returns>A tuple containing the two new rooms.</returns>
    private (RectInt, RectInt) SplitRooms(RectInt room)
    {
        Debug.Log($"Splitting Room: {room}");

        // Prevent splitting if the room is too small.
        if (room.width < minRoomWidth && room.height < minRoomHeight)
        {
            Debug.LogWarning("Room too small to split.");
            return (room, room); // Return the same room twice to prevent errors.
        }

        // Calculate random split points within the room.
        int midX = room.xMin + Random.Range(minRoomWidth, room.width - minRoomWidth);
        int midY = room.yMin + Random.Range(minRoomHeight, room.height - minRoomHeight);

        // Determine the split direction based on room dimensions.
        bool splitVertically = room.width >= minRoomWidth && (room.height < minRoomHeight || Random.value > 0.5f);

        Debug.Log($"Splitting direction: {(splitVertically ? "Vertical" : "Horizontal")}");
        Debug.Log($"MidX: {midX}, MidY: {midY}");

        RectInt a, b;

        // Perform the split based on the chosen direction.
        if (splitVertically)
        {
            a = new RectInt(room.xMin, room.yMin, midX - room.xMin, room.height); // Left room.
            b = new RectInt(midX, room.yMin, room.xMax - midX, room.height); // Right room.
        }
        else
        {
            a = new RectInt(room.xMin, room.yMin, room.width, midY - room.yMin); // Bottom room.
            b = new RectInt(room.xMin, midY, room.width, room.yMax - midY); // Top room.
        }

        // Add the new rooms to the list and remove the original room.
        _roomsArray.Add(a);
        _roomsArray.Add(b);
        _roomsArray.Remove(room);

        // Debug visualization of the new rooms.
        AlgorithmsUtils.DebugRectInt(a, Color.blue, 10, true, 0);
        AlgorithmsUtils.DebugRectInt(b, Color.blue, 10, true, 0);

        return (a, b);
    }

    /// <summary>
    /// Generates doors for all rooms in the dungeon.
    /// </summary>
    private void GenerateDoors()
    {
        foreach (var room in _roomsArray)
        {
            AddDoors(room);
        }
    }

    /// <summary>
    /// Adds doors to a given room on each edge (top, bottom, left, right).
    /// </summary>
    /// <param name="room">The room to add doors to.</param>
    /// <returns>A tuple containing the four doors.</returns>
    private (RectInt, RectInt, RectInt, RectInt) AddDoors(RectInt room)
    {
        int doorSize = 2; // Size of the door (2x2).

        // Calculate random positions for doors on each edge.
        int doorX = Random.Range(room.xMin + doorSize, room.xMax - doorSize);
        int doorY = Random.Range(room.yMin + doorSize, room.yMax - doorSize);

        // Create doors on each edge.
        RectInt topDoor = new RectInt(doorX, room.yMax - doorSize / 2, doorSize, doorSize);
        RectInt bottomDoor = new RectInt(doorX, room.yMin - doorSize / 2, doorSize, doorSize);
        RectInt leftDoor = new RectInt(room.xMin - doorSize / 2, doorY, doorSize, doorSize);
        RectInt rightDoor = new RectInt(room.xMax - doorSize / 2, doorY, doorSize, doorSize);

        // Add doors to the doors list.
        _doorsArray.Add(topDoor);
        _doorsArray.Add(bottomDoor);
        _doorsArray.Add(leftDoor);
        _doorsArray.Add(rightDoor);

        // Debug visualization of the doors.
        AlgorithmsUtils.DebugRectInt(topDoor, Color.green, 10, true, 0);
        AlgorithmsUtils.DebugRectInt(bottomDoor, Color.green, 10, true, 0);
        AlgorithmsUtils.DebugRectInt(leftDoor, Color.green, 10, true, 0);
        AlgorithmsUtils.DebugRectInt(rightDoor, Color.green, 10, true, 0);

        return (topDoor, bottomDoor, leftDoor, rightDoor);
    }

    /// <summary>
    /// Deletes a random number of rooms from the dungeon.
    /// </summary>
    private void DeleteRandomRooms()
    {
        int roomsToDelete = Random.Range(1, _roomsArray.Count / 2); // Randomly choose how many rooms to delete.
        for (int i = 0; i < roomsToDelete; i++)
        {
            int index = Random.Range(0, _roomsArray.Count); // Randomly select a room to delete.
            _roomsArray.RemoveAt(index);
        }
    }

    /// <summary>
    /// Connects paths between rooms to ensure all rooms are accessible.
    /// </summary>
    private void ConnectPaths()
    {
        // TODO: Implement path connection logic (e.g., using Prim's or Kruskal's algorithm).
        Debug.Log("Paths connected from the starting room.");
    }

    /// <summary>
    /// Assigns a starting room for the player.
    /// </summary>
    private void AssignStartingRoom()
    {
        int startingRoomIndex = Random.Range(0, _roomsArray.Count); // Randomly select a starting room.
        Debug.Log($"Starting Room: {_roomsArray[startingRoomIndex]}");
    }

    /// <summary>
    /// Clears all rooms and doors from the dungeon.
    /// </summary>
    [Button("Clear Dungeon Rooms", EButtonEnableMode.Playmode)]
    private void ClearDungeon()
    {
        _roomsArray.Clear();
        _doorsArray.Clear();
        _currentRoomDupIndex = 0;
        Debug.Log("Dungeon cleared.");
    }
}*/