using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TMPro for interaction with TextMeshPro text
using TMPro;
//using Eppy; //ADDED ??DO WE NEED THIS??

public class ConstraintSolver : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    //The patterns to build the patternlibrary from
    private List<GameObject> _goPatterns;
    [SerializeField]
    private Vector3Int _gridDimensions;
    [SerializeField]
    private float _tileSize;

    #endregion
    #region public fields


    #endregion

    #region private fields
    Tile[,,] _tileGrid;
    List<TilePattern> _patternLibrary; //?? WHERE DOES THIS GO??
    List<Connection> _connections;




    #endregion
    #region constructors
    void Start()
    {
        //Add all connections
        _connections = new List<Connection>();

        _connections.Add(new Connection("conPink"));
        _connections.Add(new Connection("conYellow"));
        _connections.Add(new Connection("conBlue"));            //ADDED
        _connections.Add(new Connection("conOrange"));        //ADDED
        _connections.Add(new Connection("conCyan"));            //ADDED
        _connections.Add(new Connection("conGreen"));          //ADDED
        _connections.Add(new Connection("conBlack"));          //ADDED
        //If you make more connection, add them here

        //Add all patterns
        _patternLibrary = new List<TilePattern>();
        foreach (var goPattern in _goPatterns)
        {
            _patternLibrary.Add(new TilePattern(_patternLibrary.Count, goPattern, _connections));
        }

        //Set up the tile grid
        MakeTiles();

    }


    #endregion
    #region public functions


    #endregion
    #region private functions


    /// <summary>
    /// Create the tile grid
    /// </summary>
    private void MakeTiles()
    {
        _tileGrid = new Tile[_gridDimensions.x, _gridDimensions.y, _gridDimensions.z];
        for (int x = 0; x < _gridDimensions.x; x++)
        {
            for (int y = 0; y < _gridDimensions.y; y++)
            {
                for (int z = 0; z < _gridDimensions.z; z++)
                {
                    _tileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary, this);
                }
            }
        }
    }

    private void FillGridRandom()
    {
        //Loop over all the tiles
        ////Assign a random pattern per tile
    }

    /// <summary>
    /// Run one step of the WaveFunctionCollapse. Put this in a loop to solve the entire grid.
    /// </summary>
    private void WaveFunctionCollapseStep()
    {
        List<Tile> unsetTiles = GetUnsetTiles();
        //Check if there still are tiles to set
        if (unsetTiles.Count == 0)
        {
            Debug.Log("all tiles are set");
            return;
        }


        //Count how many possible patterns there are
        //Find all the tiles with the least amount of possible patterns
        //Select a random tile out of this list
        //Get the tiles with the least amount of possible patterns
        List<Tile> lowestTiles = new List<Tile>();
        int lowestTile = int.MaxValue;

        foreach (Tile tile in unsetTiles)
        {
            if (tile.NumberOfPossiblePatterns < lowestTile)
            {
                lowestTiles = new List<Tile>();

                lowestTile = tile.NumberOfPossiblePatterns;
            }
            if (tile.NumberOfPossiblePatterns == lowestTile)
            {
                lowestTiles.Add(tile);
            }
        }

        //Select a random tile out of the list
        int rndIndex = Random.Range(0, lowestTiles.Count);
        Tile tileToSet = lowestTiles[rndIndex];


        //Assign one of the possible patterns to the tile
        tileToSet.AssignRandomPossiblePattern();


        //PropogateGrid on the set tile


    }

    public void PropogateGrid(Tile setTile)
    {
        //Loop over all cartesian directions (list is in Util)
        ////Get the neighbour of the set tile in the direction
        ////Get the connection of the set tile at the direction
        ////Get all the tiles with the same connection in oposite direction
        ////Remove all the possiblePatterns in the neighbour tile that are not in the connection list. 
        ////Run the CrossreferenceConnectionPatterns() on the neighbour tile
        ////If a tile has only one possiblePattern
        //////Set the tile
        //////PropogateGrid for this tile

    }

    private Tile GetNeighbour(Tile tile, Vector3Int direction)
    {
        //Get the neighbour of a tile in a certain direction
        return null;
    }
    private List<Tile> GetUnsetTiles()
    {
        List<Tile> unsetTiles = new List<Tile>();

        //Loop over all the tiles and check which ones are not set
        foreach (var tile in GetTilesFlattened())
        {
            if (!tile.Set) unsetTiles.Add(tile);
        }
        return unsetTiles;
    }

    /// <summary>
    /// Get a flattened list of tiles
    /// </summary>
    /// <returns>list of tiles</returns>
    private List<Tile> GetTilesFlattened()
    {
        List<Tile> tiles = new List<Tile>();
        for (int x = 0; x < _gridDimensions.x; x++)
        {
            for (int y = 0; y < _gridDimensions.y; y++)
            {
                for (int z = 0; z < _gridDimensions.z; z++)
                {
                    tiles.Add(_tileGrid[x, y, z]);
                }
            }
        }
        return tiles;
    }
}


//private void PropogateGrid(Tile changedTile)            //ADDED
//{
//    //Loop over the connections of the changedTile
//    //Per connection: go to the neighbour tile in the connection direction
//    //Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

//    //If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
//    //you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

//    //If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
//    //propogate the grid for the new set tile

//}


////If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
////you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

////If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
////propogate the grid for the new set tile
#endregion

