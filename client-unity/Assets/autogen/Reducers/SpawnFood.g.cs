// THIS FILE IS AUTOMATICALLY GENERATED BY SPACETIMEDB. EDITS TO THIS FILE
// WILL NOT BE SAVED. MODIFY TABLES IN YOUR MODULE SOURCE CODE INSTEAD.

#nullable enable

using System;
using SpacetimeDB.ClientApi;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpacetimeDB.Types
{
    public sealed partial class RemoteReducers : RemoteBase
    {
        public delegate void SpawnFoodHandler(ReducerEventContext ctx, SpawnFoodTimer timer);
        public event SpawnFoodHandler? OnSpawnFood;

        public void SpawnFood(SpawnFoodTimer timer)
        {
            conn.InternalCallReducer(new Reducer.SpawnFood(timer), this.SetCallReducerFlags.SpawnFoodFlags);
        }

        public bool InvokeSpawnFood(ReducerEventContext ctx, Reducer.SpawnFood args)
        {
            if (OnSpawnFood == null) return false;
            OnSpawnFood(
                ctx,
                args.Timer
            );
            return true;
        }
    }

    public abstract partial class Reducer
    {
        [SpacetimeDB.Type]
        [DataContract]
        public sealed partial class SpawnFood : Reducer, IReducerArgs
        {
            [DataMember(Name = "_timer")]
            public SpawnFoodTimer Timer;

            public SpawnFood(SpawnFoodTimer Timer)
            {
                this.Timer = Timer;
            }

            public SpawnFood()
            {
                this.Timer = new();
            }

            string IReducerArgs.ReducerName => "SpawnFood";
        }
    }

    public sealed partial class SetReducerFlags
    {
        internal CallReducerFlags SpawnFoodFlags;
        public void SpawnFood(CallReducerFlags flags) => SpawnFoodFlags = flags;
    }
}
