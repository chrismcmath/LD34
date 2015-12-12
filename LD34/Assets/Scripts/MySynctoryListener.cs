using UnityEngine;
using System.Collections;

namespace Synctory.Routers {
    public class MySynctoryListener : SynctoryRouter {

        public int LocationCount = 3;

        protected override void RegisterWithSynctory() {
            for (int i = 0; i <= 3; i++) {
                Synctory.RegisterRouter(i, this);
            }
        }
    }
}

