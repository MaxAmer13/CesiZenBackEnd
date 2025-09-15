using System.Threading.Tasks;
using Moq;
using Xunit;
using CesiZenBackEnd.Services;
using CesiZenBackEnd.Services.Abstraction;
using CesiZenBackEnd.Infrastructure.Repositories.Abstraction;
using CesiZenBackEnd.Core.Entities;
using CesiZenBackEnd.Core.DTO;

namespace CesiZenBackEndTests;

public class UserServiceTests
{
    // Helper pour créer un Utilisateur simple
    private Utilisateur CreateUser(int id = 1, string email = "test@example.com", string password = "passwd")
        => new Utilisateur
        {
            Id = id,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Prenom = "Prénom",
            Nom = "Nom",
            RoleId = 1
        };

    [Fact]
    public async Task RegisterAsync_Success_WhenEmailNotUsed()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Email = "newuser@example.com",
            Password = "Password123!",
            Prenom = "Jean",
            Nom = "Dupont",
            RoleId = 1
        };

        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email))
            .ReturnsAsync((Utilisateur?)null);

        // When AddAsync is called, simulate EF that will set an Id (optional)
        userRepoMock.Setup(r => r.AddAsync(It.IsAny<Utilisateur>()))
            .Returns(Task.CompletedTask)
            .Callback<Utilisateur>(u => u.Id = 42);

        userRepoMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.RegisterAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(42, result.Id); // set by callback
        Assert.Equal(dto.Email, result.Email);
        userRepoMock.Verify(r => r.GetByEmailAsync(dto.Email), Times.Once);
        userRepoMock.Verify(r => r.AddAsync(It.IsAny<Utilisateur>()), Times.Once);
        userRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ThrowsArgumentException_WhenEmailAlreadyUsed()
    {
        // Arrange
        var dto = new RegisterUserDto
        {
            Email = "exists@example.com",
            Password = "Password123!",
            Prenom = "Jean",
            Nom = "Dupont",
            RoleId = 1
        };

        var existing = CreateUser(email: dto.Email);
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email))
            .ReturnsAsync(existing);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.RegisterAsync(dto));
        userRepoMock.Verify(r => r.GetByEmailAsync(dto.Email), Times.Once);
        userRepoMock.Verify(r => r.AddAsync(It.IsAny<Utilisateur>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ReturnsSuccess_WhenCredentialsValid()
    {
        // Arrange
        var plainPassword = "MySecret!";
        var user = CreateUser(email: "login@example.com", password: plainPassword);

        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetByEmailAsync(user.Email))
            .ReturnsAsync(user);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        var dto = new LoginUserDto { Email = user.Email, Password = plainPassword };

        // Act
        var result = await service.LoginAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(user.Email, result.Email);
        userRepoMock.Verify(r => r.GetByEmailAsync(user.Email), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ReturnsAuthFailed_WhenUserNotFound()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((Utilisateur?)null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        var dto = new LoginUserDto { Email = "unknown@example.com", Password = "x" };

        // Act
        var result = await service.LoginAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("AUTH_FAILED", result.ErrorCode);
    }

    [Fact]
    public async Task LoginAsync_ReturnsAuthFailed_WhenPasswordInvalid()
    {
        // Arrange
        var user = CreateUser(email: "login2@example.com", password: "RightPassword!");
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetByEmailAsync(user.Email))
            .ReturnsAsync(user);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        var dto = new LoginUserDto { Email = user.Email, Password = "WrongPassword!" };

        // Act
        var result = await service.LoginAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("AUTH_FAILED", result.ErrorCode);
    }

    [Fact]
    public async Task GetUserById_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetUserById(It.IsAny<int>()))
            .ReturnsAsync((Utilisateur?)null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetUserById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserById_ReturnsUserBasicDto_WhenFound()
    {
        // Arrange
        var user = CreateUser(id: 10, email: "u@example.com");
        user.Adresse = "Rue de Test";
        user.Active = true;
        user.DateCreation = System.DateTime.UtcNow;
        user.RoleId = 2;

        var userRepoMock = new Mock<IUserRepository>();
        userRepoMock.Setup(r => r.GetUserById(user.Id))
            .ReturnsAsync(user);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Users).Returns(userRepoMock.Object);

        var service = new UserService(userRepoMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await service.GetUserById(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Nom, result.Nom);
        Assert.Equal(user.Prenom, result.Prenom);
        Assert.Equal(user.Adresse, result.Adresse);
        Assert.Equal(user.Active, result.EstActifUtil);
        Assert.Equal(user.RoleId, result.IdRole);
    }
}
