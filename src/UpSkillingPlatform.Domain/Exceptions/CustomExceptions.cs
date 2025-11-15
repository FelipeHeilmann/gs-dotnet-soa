namespace UpSkillingPlatform.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class UsuarioNaoEncontradoException : NotFoundException
{
    public UsuarioNaoEncontradoException(long id) 
        : base($"Usuário com ID {id} não foi encontrado.") { }
}

public class TrilhaNaoEncontradaException : NotFoundException
{
    public TrilhaNaoEncontradaException(long id) 
        : base($"Trilha com ID {id} não foi encontrada.") { }
}

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException(string email) 
        : base($"O email '{email}' já está cadastrado no sistema.") { }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}

public class MatriculaNaoEncontradaException : NotFoundException
{
    public MatriculaNaoEncontradaException(long id) 
        : base($"Matrícula com ID {id} não foi encontrada.") { }
}

public class MatriculaJaExisteException : Exception
{
    public MatriculaJaExisteException(long usuarioId, long trilhaId) 
        : base($"O usuário já possui uma matrícula ativa na trilha especificada.") { }
}

public class MatriculaInvalidaException : ValidationException
{
    public MatriculaInvalidaException(string message) : base(message) { }
}
