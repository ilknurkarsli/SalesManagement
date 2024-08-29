using BaSalesManagementApp.Dtos.StudentDTOs;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IStudentService
    {
        Task<IDataResult<StudentDTO>> AddAsync(StudentCreateDTO studentCreateDTO);
        Task<IDataResult<StudentDTO>> GetByIdAsync(Guid studentId);
        Task<IResult> DeleteAsync(Guid studentId);
        Task<IDataResult<List<StudentListDTO>>> GetAllAsync();
    }
}
