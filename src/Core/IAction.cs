using System.Collections;

namespace FinalEmblem.Core
{
    public interface IAction
    {
        IEnumerator Execute(Unit unit);
    }
}

