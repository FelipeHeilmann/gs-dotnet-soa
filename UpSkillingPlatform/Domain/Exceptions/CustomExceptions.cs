namespace UpSkillingPlatform.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}

public class UsuarioNaoEncontradoException : NotFoundException
{
    public UsuarioNaoEncontradoException(long id) 
        : base($"Usuário com ID {id} não foi encontrado.")
    {
    }
}

public class TrilhaNaoEncontradaException : NotFoundException
{
    public TrilhaNaoEncontradaException(long id) 
        : base($"Trilha com ID {id} não foi encontrada.")
    {
    }
}

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string email) 
        : base($"O email '{email}' já está cadastrado no sistema.")
    {
    }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
