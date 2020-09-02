using System.Collections.Generic;

namespace Model
{
    class Category : MyData
    {
        public List<int> ParentIDs { get; set; }
        public List<int> CategoryMembers { get; set; }
    }
}
