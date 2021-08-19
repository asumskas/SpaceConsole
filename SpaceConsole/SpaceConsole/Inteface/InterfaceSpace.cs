using SpaceConsole.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceConsole
{
    interface InterfaceSpace
    {

        Tuple<float,float> GetLocation(float d1, float d2, float d3);
        string GetMessage(string[] Message1, string[] Message2, string[] Message3);



    }
}
