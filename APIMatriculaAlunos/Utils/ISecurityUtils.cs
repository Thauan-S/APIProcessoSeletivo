namespace APIMatriculaAlunos.Utils
{
    public interface ISecurityUtils
    {
            void VerifyOwnerShip(string resourceOwnerId);
    }
}
