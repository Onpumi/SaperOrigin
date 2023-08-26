using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerMines
{
    private int _maxCountMines;
    private readonly GameField _gameField;
    private readonly Cell[,] _cells;
    private readonly int[] _firstIndexes;
    public int CountMines { get; private set; }
    public int CountFlags { get; private set; }

    public ContainerMines(GameField gameField, Cell[,] cells, int[] firstIndexes)
    {
        _gameField = gameField;
        _cells = cells;
        _firstIndexes = firstIndexes;
    }

    public void GenerateMines(int percentMine, int countCells, FieldCellData fieldCellData)
    {
        percentMine = _gameField.GameState.GameFieldData.PercentMine;
        _maxCountMines = countCells * percentMine / 100;
        List<int>[] arrayIndexes = new List<int>[fieldCellData.CountRows];

        for (int j = 0; _maxCountMines > 0; j++)
        {
            if (j >= fieldCellData.CountColumns - 1) j = 0;
            if (j < 0 || j > fieldCellData.CountRows - 1) throw new IndexOutOfRangeException(j.ToString());
            if (arrayIndexes[j] == null) arrayIndexes[j] = new List<int>();
            var indexRandom = UnityEngine.Random.Range(0, fieldCellData.CountRows);
            while (DeniedSetMines(indexRandom, j))
            {
                indexRandom = UnityEngine.Random.Range(0, fieldCellData.CountRows);
            }

            if (arrayIndexes[j].Count > 0)
            {
                var result = true;
                foreach (var index in arrayIndexes[j])
                {
                    if (indexRandom == index) result = false;
                }

                if (result == false)
                    continue;
            }

            arrayIndexes[j].Add(indexRandom);
            var cell = _cells[indexRandom, j];
            if (cell is not null)
            {
                cell.CreateMine(-1);
            }

            CountMines++;
            _maxCountMines--;
        }

        CountFlags = CountMines;
        _gameField.DisplayCountMines(CountMines);
    }


    private bool DeniedSetMines(int i, int j)
    {
        bool result = true;
        if (
            ((i > _firstIndexes[0] + 1 || i < _firstIndexes[0] - 1) ||
             (j > _firstIndexes[1] + 1 || j < _firstIndexes[1] - 1)
            )
        )
            result = result & false;
        else result = result & true;
        return result;
    }

    public void SetCountFlags(int value)
    {
        if (CountFlags + value >= 0)
        {
            CountFlags += value;
        }
    }
}