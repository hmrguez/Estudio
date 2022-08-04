public delegate bool FileFilter(IFile file);

public class Exam
{
    public static IFileSystem CreateFileSystem() => new FileSystem();
}

#region "File and Folder"

//Excepciones para controlar mejor el FileSystem
public class NotAFolderException : Exception { }
public class NotAFileException : Exception { }
public class NotAFileOrFolderException : Exception { }

public interface IFile
{
    int Size { get; set; }
    string Name { get; set; }
}

public class File : IFile
{
    public File(int size, string name)
    {
        Size = size;
        Name = name;
    }

    public int Size { get; set; }
    public string Name { get; set; }
}

public interface IFolder
{
    string Name { get; set; }

    IFile CreateFile(string name, int size);
    IFolder CreateFolder(string name);
    IEnumerable<IFile> GetFiles();
    IEnumerable<IFolder> GetFolders();
    int TotalSize();
}

public class Folder : IFolder
{
    public Folder(string name)
    {
        Name = name;
        Folders = new();
        Files = new();
    }

    public string Name { get; set; }
    public List<Folder> Folders { get; }
    public List<File> Files { get; }

    public IFile CreateFile(string name, int size)
    {
        var x = new File(size, name);
        Files.Add(x);
        return x;
    }

    public IFolder CreateFolder(string name)
    {
        var x = new Folder(name);
        Folders.Add(x);
        return x;
    }

    public IEnumerable<IFile> GetFiles()
    {
        for (int i = 0; i < Files.OrderBy(x=>x.Name).Count(); i++) yield return Files.OrderBy(x=>x.Name).ToArray()[i];
    }

    public IEnumerable<IFolder> GetFolders()
    {
        for (int i = 0; i < Folders.OrderBy(x=>x.Name).Count(); i++) yield return Folders.OrderBy(x=>x.Name).ToArray()[i];
    }

    //El tamano total es el de todos los archivos que tenga la carpeta mas el tamano total de todas sus carpetas
    public int TotalSize() => Files.Sum(x => x.Size) + Folders.Sum(x => x.TotalSize());
}


public interface IFileSystem
{
    void Copy(string origin, string destination);
    void Delete(string path);
    IEnumerable<IFile> Find(FileFilter filter);
    IFile GetFile(string path);
    IFolder GetFolder(string path);
    IFileSystem GetRoot(string path);
    void Move(string origin, string destination);
}

#endregion

public class FileSystem : IFileSystem
{
    public Folder Root { get; set; }
    public FileSystem() => Root = new Folder("/");
    public static string[] Parse(string path) => path.Split('/', StringSplitOptions.RemoveEmptyEntries);
    public static string[] GetParentDirectory(string path) => Parse(path).Where(x => x != Parse(path).Last()).ToArray();

    public IEnumerable<IFile> Find(FileFilter filter)
    {
        //Buscame todos los archivos en la raiz que cumplan con el criterio
        foreach (var item in Root.Files.Where(x => filter(x)).OrderBy(x => x.Name)) yield return item;

        //Llamame al mismo metodo pero un FileSystem centrado en cada carpeta de la raiz
        for (int i = 0; i < Root.Folders.Count; i++)
            foreach (var item in (new FileSystem() { Root = Root.Folders[i] }).Find(filter))
                yield return item;
    }


    //Llama al metodo de recursividad de cola con ese mismo nombre
    public IFolder GetFolder(string path) => (path == "/") ? Root : GetFolder(Parse(path), 0);
    public IFolder GetFolder(string[] path, int pos)
    {
        //Cuando la posicion ya llego al final entonces es que ese FileSystem esta centrado en la carpeta que quiero, por tanto devuelve su raiz
        if (path.Length == pos) return Root;
        else
        {
            //Mientras tanto buscame por todas las carpetas de la raiz de este FileSystem y dime si hay alguna cuyo nombre coincida con el path en esta posicion
            //Si la encontro llamame la recursividad de cola en la siguiente posicion del path pero centrame el FileSystem en la carpeta que coincidio con el nombre
            for (int i = 0; i < Root.Folders.Count; i++)
                if (Root.Folders[i].Name == path[pos])
                    return (new FileSystem() { Root = Root.Folders[i] }).GetFolder(path, pos + 1);

            //Si llego a este punto es que no pudo devolver nada y entonces tira excepcion
            throw new NotAFolderException();
        }
    }
    public IFile GetFile(string path)
    {
        //Parseame el camino (Splittearlo por las /)
        string[] parsed = Parse(path);

        //Buscame al posible padre de esa direccion (el mismo array menos el ultimo)
        var allButLast = parsed.Where(x => x != parsed.Last()).ToArray();

        //Si esa direccion no tiene elementos entonces es que el archivo esta en la raiz
        //Por tanto buscame en la raiz todos los archivos y revisa si hay alguno que tenga el mismo nombre y devuelvelo
        if (allButLast.Length == 0)
        {
            for (int i = 0; i < Root.Files.Count; i++)
                if (Root.Files[i].Name == parsed.Last())
                    return Root.Files[i];
        }
                    
        //Sino buscame la carpeta padre de esa direccion y haz lo mismo pero con esa carpeta
        else
        {
            var folder = GetFolder(allButLast, 0) as Folder;
            for (int i = 0; i < folder!.Files.Count; i++)
                if (folder.Files[i].Name == parsed.Last())
                    return folder.Files[i];
        }
        //Si no pudo devolver nada es que ese archivo no existe y por tanto tira excepcion
        throw new NotAFileException();
    }
    
    //Devuelve un FileSystem que tiene como carpeta raiz el camino que me dicen 
    public IFileSystem GetRoot(string path) => new FileSystem() { Root = (GetFolder(path) as Folder)! };

    public void Delete(string path)
    {
        //Coge la carpeta que representa el padre de este directorio
        var directory = GetParentDirectory(path);
        var root = GetFolder(directory, 0) as Folder;
        var last = Parse(path).Last();

        //Revisa entre todos los archivos y carpetas de esa carpeta por el que tenga el mismo nombre que el ultimo elemento del directorio y lo borra
        bool cumple = false;
        for (int i = 0; i < root!.Files.Count; i++) if (root.Files[i].Name == last) cumple = root.Files.Remove(root.Files[i]);
        for (int i = 0; i < root.Folders.Count; i++) if (root.Folders[i].Name == last) cumple = root.Folders.Remove(root.Folders[i]);
        if (!cumple) throw new NotAFileOrFolderException();
    }
    public void Move(string origin, string destination)
    {
        //Mover es simplemente copiar y borrar el origen
        Copy(origin, destination);
        Delete(origin);
    }


    public void Copy(string origin, string destination)
    {
        //Coge la carpeta que representa el padre de este directorio origen y la carpeta que representa el destino
        var end = GetFolder(destination) as Folder;
        var directory = GetFolder(GetParentDirectory(origin), 0) as Folder;
        var last = Parse(origin).Last();

        //Revisa entre los archivos y carpetas por el que coincida con el nombre del ultimo valor del directorio origen y lo copia a la carpeta destino
        bool cumple = false;
        for (int i = 0; i < directory!.Files.Count; i++) 
            if (directory.Files[i].Name == last)
            {
                cumple = true;
                CopyFile(directory.Files[i], end!);
            }
        for (int i = 0; i < directory.Folders.Count; i++) 
            if (directory.Folders[i].Name == last)
            {
                cumple = true;
                CopyFolder(directory.Folders[i], end!);
            }
        if (!cumple) throw new NotAFileOrFolderException();
    }
    public void CopyFile(File file, Folder destination)
    {
        //Si en la carpeta de destino ya hay un archivo con el mismo nombre, borralo y pon el nuevo
        if (destination.Files.Exists(x => x.Name == file.Name))
        {
            var same = destination.Files.Find(x => x.Name == file.Name);
            destination.Files.Remove(same!);
            destination.CreateFile(file.Name, file.Size);
        }
        //Sino simplemente crea un nuevo archivo igual al anterior
        else destination.CreateFile(file.Name, file.Size);
    }
    public void CopyFolder(Folder folder, Folder destination)
    {
        //Si ya hay una carpeta en el destino con el mismo nombre entonces copiame todos los archivos y carpetas que estan dentro a esa
        if (destination.Folders.Exists(x => x.Name == folder.Name))
        {
            var same = destination.Folders.Find(x => x.Name == folder.Name);
            for (int i = 0; i < folder.Files.Count; i++) CopyFile(folder.Files[i], same!);
            for (int i = 0; i < folder.Folders.Count; i++) CopyFolder(folder.Folders[i], same!);
        }
        //Sino entonces creame una nueva carpeta con el mismo nombre que la que estoy copiando y copia todos los archivos y carpetas que contenga
        else
        {
            var newFolder = destination.CreateFolder(folder.Name) as Folder;
            for (int i = 0; i < folder.Files.Count; i++) CopyFile(folder.Files[i], newFolder!);
            for (int i = 0; i < folder.Folders.Count; i++) CopyFolder(folder.Folders[i], newFolder!);
        }
    }
}


