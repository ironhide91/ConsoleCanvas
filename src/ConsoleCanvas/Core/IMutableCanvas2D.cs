using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface IMutableCanvas2D
    {
        bool Initialize(Dimension2D dimension);
        void Draw(DrawUnit unit);
        void Draw(IEnumerable<DrawUnit> units);
        void Reset();
    }
}