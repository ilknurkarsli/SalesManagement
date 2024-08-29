
using BaSalesManagementApp.Dtos.StudentDTOs;


namespace BaSalesManagementApp.Business.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IDataResult<StudentDTO>> AddAsync(StudentCreateDTO studentCreateDTO)
        {
            var newStudent = studentCreateDTO.Adapt<Student>();
            await _studentRepository.AddAsync(newStudent);
            await _studentRepository.SaveChangeAsync();
            return new SuccessDataResult<StudentDTO>(newStudent.Adapt<StudentDTO>(), "STUDENT_ADDED_SUCCESS");
        }

        public async Task<IResult> DeleteAsync(Guid studentId)
        {
            var deletingStudent = await _studentRepository.GetByIdAsync(studentId);
            await _studentRepository.DeleteAsync(deletingStudent);
            await _studentRepository.SaveChangeAsync();
            return new SuccessResult("STUDENT_DELETED_SUCCESS");
        }

        public async Task<IDataResult<List<StudentListDTO>>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return new SuccessDataResult<List<StudentListDTO>>( students.Adapt<List<StudentListDTO>>(), "STUDENT_LISTED_SUCCESS");
        }

        public async Task<IDataResult<StudentDTO>> GetByIdAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return new SuccessDataResult<StudentDTO>(student.Adapt<StudentDTO>(), "STUDENT_BROUGHT_SUCCESS");
        }
    }
}
