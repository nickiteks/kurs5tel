using System;

public static class SceneNotFoundException 
{
    public static string SceneNotFound(string name)
    {
        return "Сцена с таким именем не найдена " + name;
    }
}
