using System.Collections;
using System.Collections.Generic;

namespace Utilities.EventSystem
{
    public class BuildTowerSelectedEvent : EventBase<BuildTowerSelectedEvent>
    {
        public TowerData TowerData;
    }
}
