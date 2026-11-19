namespace DIAndSingleton;

public class StudentOperations
{
    // Lista studentów w pamięci na której będziemy pracować
    private List<Student> _students = [];
    private DateTime _godzinaUtworzeniaSerwisu = DateTime.Now;

    // Pobiera wszystkich studentów (_students) albo tych któych numer albumu
    // zaczyna się na numer wpisany w wyszukiwanie
    public List<Student> GetStudents(string? album)
    {
        if (string.IsNullOrWhiteSpace(album))
            return _students;
        else
            return _students.Where(s => s.AlbumNumber.StartsWith(album)).ToList();
    }

    // Dodaje studenta do listy (do listy _students)
    public Result AddNewStudent(CreateNewStudentCommand command)
    {
        var errors = new List<string>();

        // Walidacja czy imię, nazwisko i album zostały podane i czy nie są puste

        if (string.IsNullOrWhiteSpace(command.FirstName))
            errors.Add("Nie podano imienia");

        if (string.IsNullOrWhiteSpace(command.LastName))
            errors.Add("Nie podano nazwiska");

        if (string.IsNullOrWhiteSpace(command.AlbumNumber))
            errors.Add("Nie podano numeru albumu");

        if (errors.Count > 0)
            return new Result { Success = false, Error = string.Join(", ", errors) };

        // Walidacja czy taki student już istnieje
        var studentInDb = _students.FirstOrDefault(s => s.AlbumNumber == command.AlbumNumber);

        if (studentInDb is not null)
            return new Result { Success = false, Error = $"Student o albumie {command.AlbumNumber} już istnieje !"  };

        var student = new Student
        {
            FirstName = command.FirstName!,
            LastName = command.LastName!,
            AlbumNumber = command.AlbumNumber!
        };

        _students.Add(student);
        return new Result { Success = true, Data = $"Student został dodany. StudentId: {student.StudentId.ToString()}" };
    }


}
