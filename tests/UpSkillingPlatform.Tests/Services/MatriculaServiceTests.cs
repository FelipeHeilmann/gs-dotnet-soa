using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Application.Services;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Tests.Services;

[TestClass]
public class MatriculaServiceTests
{
    private Mock<IMatriculaRepository> _matriculaRepositoryMock = null!;
    private Mock<IUsuarioRepository> _usuarioRepositoryMock = null!;
    private Mock<ITrilhaRepository> _trilhaRepositoryMock = null!;
    private MatriculaService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _trilhaRepositoryMock = new Mock<ITrilhaRepository>();
        _service = new MatriculaService(
            _matriculaRepositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _trilhaRepositoryMock.Object);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrowException_WhenUsuarioNotFound()
    {
        var dto = new CreateMatriculaDto { UsuarioId = 999, TrilhaId = 1 };
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Usuario?)null);

        await Assert.ThrowsExceptionAsync<UsuarioNaoEncontradoException>(() => _service.CreateAsync(dto));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrowException_WhenTrilhaNotFound()
    {
        var dto = new CreateMatriculaDto { UsuarioId = 1, TrilhaId = 999 };
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _trilhaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trilha?)null);

        await Assert.ThrowsExceptionAsync<TrilhaNaoEncontradaException>(() => _service.CreateAsync(dto));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrowException_WhenMatriculaJaExiste()
    {
        var dto = new CreateMatriculaDto { UsuarioId = 1, TrilhaId = 1 };
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _trilhaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trilha);
        _matriculaRepositoryMock.Setup(r => r.ExisteMatriculaAtivaAsync(1, 1)).ReturnsAsync(true);

        await Assert.ThrowsExceptionAsync<MatriculaJaExisteException>(() => _service.CreateAsync(dto));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldCreateMatricula_WhenDataIsValid()
    {
        var dto = new CreateMatriculaDto { UsuarioId = 1, TrilhaId = 1 };
        var usuario = new Usuario { Id = 1, Nome = "Test User", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            DataInscricao = DateTime.Now, 
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _trilhaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trilha);
        _matriculaRepositoryMock.Setup(r => r.ExisteMatriculaAtivaAsync(1, 1)).ReturnsAsync(false);
        _matriculaRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Matricula>())).ReturnsAsync(matricula);

        var result = await _service.CreateAsync(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Ativa", result.Status);
        Assert.AreEqual("Test User", result.NomeUsuario);
        Assert.AreEqual("Trilha Test", result.NomeTrilha);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldThrowException_WhenMatriculaNotFound()
    {
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Matricula?)null);

        await Assert.ThrowsExceptionAsync<MatriculaNaoEncontradaException>(() => _service.GetByIdAsync(999));
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnMatricula_WhenMatriculaExists()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test User", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            DataInscricao = DateTime.Now, 
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);

        var result = await _service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Ativa", result.Status);
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ShouldThrowException_WhenUsuarioNotFound()
    {
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Usuario?)null);

        await Assert.ThrowsExceptionAsync<UsuarioNaoEncontradoException>(() => _service.GetByUsuarioIdAsync(999));
    }

    [TestMethod]
    public async Task GetByUsuarioIdAsync_ShouldReturnMatriculas_WhenUsuarioExists()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test User", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matriculas = new List<Matricula>
        {
            new Matricula 
            { 
                Id = 1, 
                UsuarioId = 1, 
                TrilhaId = 1, 
                DataInscricao = DateTime.Now, 
                Status = "Ativa",
                Usuario = usuario,
                Trilha = trilha
            }
        };
        
        _usuarioRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _matriculaRepositoryMock.Setup(r => r.GetByUsuarioIdAsync(1)).ReturnsAsync(matriculas);

        var result = await _service.GetByUsuarioIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public async Task GetByTrilhaIdAsync_ShouldThrowException_WhenTrilhaNotFound()
    {
        _trilhaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trilha?)null);

        await Assert.ThrowsExceptionAsync<TrilhaNaoEncontradaException>(() => _service.GetByTrilhaIdAsync(999));
    }

    [TestMethod]
    public async Task CancelarMatriculaAsync_ShouldThrowException_WhenMatriculaNotFound()
    {
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Matricula?)null);

        await Assert.ThrowsExceptionAsync<MatriculaNaoEncontradaException>(() => _service.CancelarMatriculaAsync(999));
    }

    [TestMethod]
    public async Task CancelarMatriculaAsync_ShouldThrowException_WhenMatriculaJaCancelada()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Cancelada",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);

        await Assert.ThrowsExceptionAsync<MatriculaInvalidaException>(() => _service.CancelarMatriculaAsync(1));
    }

    [TestMethod]
    public async Task CancelarMatriculaAsync_ShouldThrowException_WhenMatriculaConcluida()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Concluída",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);

        await Assert.ThrowsExceptionAsync<MatriculaInvalidaException>(() => _service.CancelarMatriculaAsync(1));
    }

    [TestMethod]
    public async Task CancelarMatriculaAsync_ShouldCancelMatricula_WhenMatriculaAtiva()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _matriculaRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Matricula>())).ReturnsAsync(matricula);

        var result = await _service.CancelarMatriculaAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Cancelada", matricula.Status);
    }

    [TestMethod]
    public async Task ConcluirMatriculaAsync_ShouldThrowException_WhenMatriculaCancelada()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Cancelada",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);

        await Assert.ThrowsExceptionAsync<MatriculaInvalidaException>(() => _service.ConcluirMatriculaAsync(1));
    }

    [TestMethod]
    public async Task ConcluirMatriculaAsync_ShouldConcluirMatricula_WhenMatriculaAtiva()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _matriculaRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Matricula>())).ReturnsAsync(matricula);

        var result = await _service.ConcluirMatriculaAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual("Concluída", matricula.Status);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldThrowException_WhenMatriculaNotFound()
    {
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Matricula?)null);

        await Assert.ThrowsExceptionAsync<MatriculaNaoEncontradaException>(() => _service.DeleteAsync(999));
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldDeleteMatricula_WhenMatriculaExists()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com" };
        var trilha = new Trilha { Id = 1, Nome = "Trilha Test", Nivel = "Iniciante", CargaHoraria = 40 };
        var matricula = new Matricula 
        { 
            Id = 1, 
            UsuarioId = 1, 
            TrilhaId = 1, 
            Status = "Ativa",
            Usuario = usuario,
            Trilha = trilha
        };
        
        _matriculaRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(matricula);
        _matriculaRepositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _service.DeleteAsync(1);

        _matriculaRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}
