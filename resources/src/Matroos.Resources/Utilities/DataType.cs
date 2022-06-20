using Matroos.Resources.Attributes;

namespace Matroos.Resources.Utilities;

public enum DataType
{
    [AType(typeof(bool))]
    BOOLEAN,    // 0
    [AType(typeof(string))]
    DATE,       // 1
    [AType(typeof(double))]
    DOUBLE,     // 2
    [AType(typeof(int))]
    INTEGER,    // 3
    [AType(typeof(string))]
    STRING,     // 4
    [AType(typeof(List<object>))]
    LIST        // 5
}

