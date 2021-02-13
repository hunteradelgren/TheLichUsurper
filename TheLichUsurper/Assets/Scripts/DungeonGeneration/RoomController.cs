using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;

    public int X;

    public int Y;



}



public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Dungeon";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    Room currRoom;

    void Awake()
    {
        instance = this;
    }


     void Start()
        {
            LoadRoom("DungeonBase", 0, 0);
            LoadRoom("DungeonBottom", 0, 1);
            LoadRoom("DungeonTop", 0, -1);
            LoadRoom("DungeonLeft", 1, 0);
            LoadRoom("DungeonRight", -1, 0);
        }



    void Update()
        {
            UpdateRoomQueue();
        }
    
    public void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));


    }


    public void LoadRoom( string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        Debug.Log("didnt Exist");
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
        Debug.Log("Loaded");

    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        Debug.Log("Loading Routine");
        string roomName = info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }

    }

    public void RegisterRoom(Room room)
    {
        Debug.Log("Registering");
        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);

        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = currentWorldName + "-" + currentLoadRoomData + " " + room.X + "," + room.Y;

        room.transform.parent = transform;

        isLoadingRoom = false;
        Debug.Log("Registered");

        if(loadedRooms.Count == 0)
        {
            CameraController.instance.currentRoom = room;
        }


        loadedRooms.Add(room);

    }

    
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;

    }
    
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currRoom = room;




    }
    
    
   

    



}
