﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using board.engine;
using board.engine.Board;
using board.engine.Movement;
using chess.engine.Chess;
using chess.engine.Chess.Entities;
using chess.engine.Chess.Pieces;
using chess.engine.Game;

namespace chess.engine
{
    public static class ChessGameConvert
    {
        public static string Serialise(ChessGame chessGameBoard)
        {
            var sb = new StringBuilder();

            for (var y = 8; y >= 1; y--)
            {
                for (var x = 1; x <= 8; x++)
                {
                    var item = chessGameBoard.Board[x - 1, y - 1];
                    var c = AsChar(item);
                    sb.Append(c);
                }
            }

            sb.Append(chessGameBoard.CurrentPlayer.ToString().First());

            CheckForCastleEligibility(chessGameBoard, sb);

            return sb.ToString();
        }

        private static void CheckForCastleEligibility(ChessGame chessGameBoard, StringBuilder sb)
        {
            LocatedItem<ChessPieceEntity> GetKing(Colours colours) =>
                chessGameBoard.BoardState.GetItem(King.StartPositionFor(colours));

            void AddCastleEligibility(IReadOnlyCollection<BoardMove> kingMoves)
            {
                bool CanCastle(ChessMoveTypes castleSide)
                {
                    return kingMoves.Any(p => p.MoveType == (int) castleSide);
                }

                sb.Append(CanCastle(ChessMoveTypes.CastleQueenSide) ? "1" : "0");
                sb.Append(CanCastle(ChessMoveTypes.CastleKingSide) ? "1" : "0");
            }

            AddCastleEligibility(GetKing(Colours.White)?.Paths.FlattenMoves().ToList() ?? new List<BoardMove>());
            AddCastleEligibility(GetKing(Colours.Black)?.Paths.FlattenMoves().ToList() ?? new List<BoardMove>());
        }

        private static char AsChar(LocatedItem<ChessPieceEntity> item)
        {
            if (item == null) return '.';

            if (item.Item.Piece == ChessPieceName.Pawn)
            {
                // TODO: Enpassant eligibility check and flag
            }

            var textRepresentation = PieceNameMapper.ToChar(item.Item.Piece, item.Item.Player);
            // TODO: Add enpassant check
            return textRepresentation;
        }

        public static ChessGame Deserialise(string boardformat69char)
        {
            if (boardformat69char.Length != 69) throw new ArgumentException($"Invalid serialised board, must be 69 characters long (yours was {boardformat69char.Length})", nameof(boardformat69char));

            // TODO: Handling of invalid formats
            int idx = 1;

            var setup = CreatePieceEntitiesSetupActions(boardformat69char.Substring(0, 64), idx);

            var whoseTurn = boardformat69char[64] == 'W'
                ? Colours.White
                : Colours.Black;

            // TODO: King eligibility stuff
            return new ChessGame(
                ChessFactory.Logger<ChessGame>(),
                ChessFactory.ChessBoardEngineProvider(),
                ChessFactory.ChessPieceEntityProvider(),
                ChessFactory.ChessGameStateService(),
                setup,
                whoseTurn
            );
        }

        private static DeserialisedBoardSetup CreatePieceEntitiesSetupActions(string pieces, int idx)
        {
            var toBePlaced = new List<Action<BoardEngine<ChessPieceEntity>>>();

            foreach (var piece in pieces)
            {
                var locX = (idx - 1) % 8 + 1;
                var locY = 8 - (idx - 1) / 8;

                if (piece != '.' && piece != ' ')
                {
                    var newPiece = BuildEntity(piece);

                    toBePlaced.Add((engine) => engine.AddPiece(newPiece, BoardLocation.At(locX, locY)));
                }

                idx++;
            }

            return new DeserialisedBoardSetup(toBePlaced);
        }

        private static ChessPieceEntity BuildEntity(char piece)
        {
            // TODO: Enpassant pawn check
            var chessPieceName = PieceNameMapper.FromChar(piece);
            var colour = char.IsUpper(piece) ? Colours.White : Colours.Black;
            var newPiece = ChessFactory.ChessPieceEntityProvider().Create(new ChessPieceEntityProvider.ChessPieceEntityFactoryTypeExtraData
            {
                PieceName = chessPieceName, Owner = colour
            });
            return newPiece;
        }

        private class DeserialisedBoardSetup : IBoardSetup<ChessPieceEntity>
        {
            private readonly IEnumerable<Action<BoardEngine<ChessPieceEntity>>> _toBePlaced;

            public DeserialisedBoardSetup(IEnumerable<Action<BoardEngine<ChessPieceEntity>>> toBePlaced)
            {
                _toBePlaced = toBePlaced;
            }


            public void SetupPieces(BoardEngine<ChessPieceEntity> engine)
            {
                foreach (var action in _toBePlaced)
                {
                    action(engine);
                }
            }
        }
    }
}