using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UpSkillingPlatform.Application.DTOs;
using UpSkillingPlatform.Application.Services;
using UpSkillingPlatform.Domain.Entities;
using UpSkillingPlatform.Domain.Exceptions;
using UpSkillingPlatform.Domain.Interfaces;

namespace UpSkillingPlatform.Tests.Services;

[TestClass]
public class TrilhaServiceTests
{
    private Mock<ITrilhaRepository> _repositoryMock = null!;
    private TrilhaService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<ITrilhaRepository>();
        _service = new TrilhaService(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldThrowException_WhenNivelInvalid()
    {
        var dto = new TrilhaCreateDto { Nome = "Test", Nivel = "INVALID", CargaHoraria = 10 };

        await Assert.ThrowsExceptionAsync<ValidationException>(() => _service.CreateAsync(dto));
    }

    [TestMethod]
    public async Task CreateAsync_ShouldCreateTrilha_WhenNivelValid()
    {
        var dto = new TrilhaCreateDto { Nome = "Test", Nivel = "INICIANTE", CargaHoraria = 10 };
        var trilha = new Trilha { Id = 1, Nome = dto.Nome, Nivel = dto.Nivel, CargaHoraria = dto.CargaHoraria };
        
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Trilha>())).ReturnsAsync(trilha);

        var result = await _service.CreateAsync(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("INICIANTE", result.Nivel);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldAccept_NivelIntermediario()
    {
        var dto = new TrilhaCreateDto { Nome = "Test", Nivel = "INTERMEDIARIO", CargaHoraria = 20 };
        var trilha = new Trilha { Id = 1, Nome = dto.Nome, Nivel = dto.Nivel, CargaHoraria = dto.CargaHoraria };
        
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Trilha>())).ReturnsAsync(trilha);

        var result = await _service.CreateAsync(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual("INTERMEDIARIO", result.Nivel);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldAccept_NivelAvancado()
    {
        var dto = new TrilhaCreateDto { Nome = "Test", Nivel = "AVANCADO", CargaHoraria = 30 };
        var trilha = new Trilha { Id = 1, Nome = dto.Nome, Nivel = dto.Nivel, CargaHoraria = dto.CargaHoraria };
        
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Trilha>())).ReturnsAsync(trilha);

        var result = await _service.CreateAsync(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual("AVANCADO", result.Nivel);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldThrowException_WhenTrilhaNotFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trilha?)null);

        await Assert.ThrowsExceptionAsync<TrilhaNaoEncontradaException>(() => _service.GetByIdAsync(999));
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnTrilha_WhenTrilhaExists()
    {
        var trilha = new Trilha { Id = 1, Nome = "Test", Nivel = "INICIANTE", CargaHoraria = 10 };
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trilha);

        var result = await _service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldThrowException_WhenTrilhaNotFound()
    {
        var dto = new TrilhaUpdateDto { Nome = "Test", Nivel = "INICIANTE", CargaHoraria = 10 };
        _repositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Trilha?)null);

        await Assert.ThrowsExceptionAsync<TrilhaNaoEncontradaException>(() => _service.UpdateAsync(999, dto));
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldThrowException_WhenNivelInvalid()
    {
        var dto = new TrilhaUpdateDto { Nome = "Test", Nivel = "INVALID", CargaHoraria = 10 };
        var trilha = new Trilha { Id = 1, Nome = "Old", Nivel = "INICIANTE", CargaHoraria = 5 };
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trilha);

        await Assert.ThrowsExceptionAsync<ValidationException>(() => _service.UpdateAsync(1, dto));
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldUpdateTrilha_WhenValid()
    {
        var dto = new TrilhaUpdateDto { Nome = "Updated", Nivel = "AVANCADO", CargaHoraria = 50 };
        var trilha = new Trilha { Id = 1, Nome = "Old", Nivel = "INICIANTE", CargaHoraria = 10 };
        
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trilha);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Trilha>())).ReturnsAsync(trilha);

        var result = await _service.UpdateAsync(1, dto);

        Assert.IsNotNull(result);
        Assert.AreEqual("Updated", result.Nome);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldThrowException_WhenTrilhaNotFound()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

        await Assert.ThrowsExceptionAsync<TrilhaNaoEncontradaException>(() => _service.DeleteAsync(999));
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldDeleteTrilha_WhenExists()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        _repositoryMock.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

        await _service.DeleteAsync(1);

        _repositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnAllTrilhas()
    {
        var trilhas = new List<Trilha>
        {
            new Trilha { Id = 1, Nome = "Test1", Nivel = "INICIANTE", CargaHoraria = 10 },
            new Trilha { Id = 2, Nome = "Test2", Nivel = "AVANCADO", CargaHoraria = 20 }
        };
        
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(trilhas);

        var result = await _service.GetAllAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }
}
