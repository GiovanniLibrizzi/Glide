using System;
using System.Collections.Generic;
using System.Text;

namespace Glide.Content {
    class Util {

        public static float Lerp(float firstFloat, float secondFloat, float by) {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}
