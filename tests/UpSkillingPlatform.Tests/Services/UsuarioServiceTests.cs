using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Application.Services;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Tests.Services;

[TestClass]
public class UsuarioServiceTests
{
    private Mock<IUsuarioRepository> _repositoryMock = null!;
    private UsuarioService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IUsuarioRepository>();
        _service = new UsuarioService(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrowException_WhenEmailExists()
    {
        var dto = new UsuarioCreateDto { Nome = "Test", Email = "test@test.com" };
        _repositoryMock.Setup(r => r.EmailExistsAsync(dto.Email)).ReturnsAsync(true);

        await Assert.ThrowsExceptionAsync<EmailJaCadastradoException>(() => _service.CreateAsync(dto));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldCreateUsuario_WhenEmailDoesNotExist()
    {
        var dto = new UsuarioCreateDto { Nome = "Test", Email = "test@test.com" };
        var usuario = new Usuario { Id = 1, Nome = dto.Nome, Email = dto.Email, DataCadastro = DateTime.Now };
        
        _repositoryMock.Setup(r => r.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Usuario>())).ReturnsAsync(usuario);

        var result = await _service.CreateAsync(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Test", result.Nome);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldThrowException_WhenUsuarioNotFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Usuario?)null);

        await Assert.ThrowsExceptionAsync<UsuarioNaoEncontradoException>(() => _service.GetByIdAsync(999));
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnUsuario_WhenUsuarioExists()
    {
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "test@test.com", DataCadastro = DateTime.Now };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);

        var result = await _service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldThrowException_WhenUsuarioNotFound()
    {
        var dto = new UsuarioUpdateDto { Nome = "Test", Email = "test@test.com" };
        _repositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Usuario?)null);

        await Assert.ThrowsExceptionAsync<UsuarioNaoEncontradoException>(() => _service.UpdateAsync(999, dto));
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldThrowException_WhenEmailExistsForOtherUser()
    {
        var dto = new UsuarioUpdateDto { Nome = "Test", Email = "test@test.com" };
        var usuario = new Usuario { Id = 1, Nome = "Test", Email = "old@test.com", DataCadastro = DateTime.Now };
        var existingUser = new Usuario { Id = 2, Nome = "Other", Email = dto.Email, DataCadastro = DateTime.Now };
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _repositoryMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(existingUser);

        await Assert.ThrowsExceptionAsync<EmailJaCadastradoException>(() => _service.UpdateAsync(1, dto));
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldUpdateUsuario_WhenValid()
    {
        var dto = new UsuarioUpdateDto { Nome = "Updated", Email = "updated@test.com", AreaAtuacao = "TI" };
        var usuario = new Usuario { Id = 1, Nome = "Old", Email = "old@test.com", DataCadastro = DateTime.Now };
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        _repositoryMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((Usuario?)null);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Usuario>())).ReturnsAsync(usuario);

        var result = await _service.UpdateAsync(1, dto);

        Assert.IsNotNull(result);
        Assert.AreEqual("Updated", result.Nome);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldThrowException_WhenUsuarioNotFound()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

        await Assert.ThrowsExceptionAsync<UsuarioNaoEncontradoException>(() => _service.DeleteAsync(999));
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldDeleteUsuario_WhenExists()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _service.DeleteAsync(1);

        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllUsuarios()
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "Test1", Email = "test1@test.com", DataCadastro = DateTime.Now },
            new Usuario { Id = 2, Nome = "Test2", Email = "test2@test.com", DataCadastro = DateTime.Now }
        };
        
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(usuarios);

        var result = await _service.GetAllAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }
}
