﻿using EntityFX.Gdcame.GameEngine.Contract;
using EntityFX.Gdcame.GameEngine.Contract.Items;

namespace EntityFX.Gdcame.GameEngine.NetworkGameEngine
{
    public interface IGameDataChangesNotifier
    {
        ///////void GameDataChanged(IGame game);
        void AutomaticRefreshed(IGame game);
        ///////void FundsDriverBought(IGame game, Item item);
    }
}