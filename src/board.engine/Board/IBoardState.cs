﻿using System;
using System.Collections.Generic;
using board.engine.Movement;

namespace board.engine.Board
{
    public interface IBoardState<TEntity> : ICloneable where TEntity : class, IBoardEntity
    {
        void PlaceEntity(BoardLocation loc, TEntity entity);
        LocatedItem<TEntity> GetItem(BoardLocation loc);

        bool IsEmpty(BoardLocation location);

        IEnumerable<LocatedItem<TEntity>> GetItems(params BoardLocation[] locations);
        IEnumerable<LocatedItem<TEntity>> GetItems(int owner);
        IEnumerable<LocatedItem<TEntity>> GetItems(int owner, int entityType);
        IEnumerable<LocatedItem<TEntity>> GetAllItems();

        void Clear();
        void Remove(BoardLocation loc);

        IEnumerable<BoardLocation> GetAllMoveDestinations(int forPlayer);
        IEnumerable<BoardLocation> GetAllItemLocations { get; }

        void RegenerateAllPaths();
        void RegeneratePaths(int owner);
        void RegeneratePaths(BoardLocation at);
    }
}