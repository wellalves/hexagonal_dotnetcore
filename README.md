# hexagonal_dotnetcore
Exemplo de arquitetura hexagonal em .net core 7

🧠 Conceito Central da Arquitetura Hexagonal
A ideia principal é separar o núcleo da aplicação (regras de negócio) das partes que mudam com mais frequência, como o banco de dados, a interface HTTP, serviços externos, UI, etc.

Essa separação é feita com Portas (Ports) e Adaptadores (Adapters).


🧩 Componentes da Arquitetura Hexagonal
Imagine a aplicação como um hexágono, onde:

Domínio:	Onde vivem as regras de negócio. Simples, pura, sem dependências externas.
Aplicação:	Orquestra os casos de uso. Usa interfaces do domínio.
Portas (Ports):	Interfaces que definem como o domínio se comunica com o exterior.
Adaptadores:	Implementações concretas dessas portas: banco de dados, APIs, CLI, etc.


🔸 1. Domínio: o Coração
Aqui ficam as entidades como Student, School, Teacher, Class, Tuition.

Exemplo:
Student.cs define a estrutura e regras que representam um estudante, mas não sabe nada sobre onde ele será salvo, nem como vai ser exibido na tela.

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SchoolId { get; set; }
}


🔸 2. Portas (Ports): as Interfaces
São interfaces que representam comportamentos esperados.

Exemplo:
public interface IStudentRepository
{
    Task SaveAsync(Student student);
    Task<Student?> GetByIdAsync(Guid id);
    Task<IEnumerable<Student>> GetAllAsync();
}

Aqui, o domínio diz:

“Para funcionar, preciso de um repositório de estudantes que saiba salvar, buscar, listar.”

Mas não importa para ele como isso será feito.


🔸 3. Aplicação: Casos de Uso
Aqui estão os serviços de aplicação que fazem a orquestração da lógica de negócio.

Exemplo: CreateStudent.cs

public class CreateStudent
{
    private readonly IStudentRepository _repository;

    public CreateStudent(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(string name, Guid schoolId)
    {
        var student = new Student { ... };
        await _repository.SaveAsync(student);
    }
}
Esse código apenas coordena a criação de um aluno, delegando o armazenamento para quem implementa a interface.

🔸 4. Adaptadores (Adapters)
Aqui implementamos as portas para algo real, como salvar em memória, banco de dados, enviar e-mail etc.

Exemplo: InMemoryStudentRepository.cs

public class InMemoryStudentRepository : IStudentRepository
{
    private List<Student> _students = new();

    public async Task SaveAsync(Student student)
    {
        _students.Add(student);
    }

    ...
}
Esse repositório implementa a interface e pode ser substituído facilmente por um EFCoreStudentRepository, por exemplo.

🔸 5. Interface do Usuário / API
A controller chama os casos de uso:
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
{
    await _useCase.ExecuteAsync(request.Name, request.SchoolId);
    return Ok(new { message = "Student created!" });
}

Aqui o Controller atua como um adaptador de entrada, traduzindo a chamada HTTP para um comando de caso de uso.

🔁 O Ciclo de Execução
Quando um usuário faz uma requisição para POST /api/student:

O Controller recebe a requisição e transforma em dados estruturados.

Chama o caso de uso (CreateStudent) com esses dados.

O caso de uso cria a entidade e chama o repositório, via interface (IStudentRepository).

O adaptador (InMemory) salva os dados.

A resposta volta para a API → usuário.


🎯 Vantagens da Arquitetura Hexagonal
Testabilidade:	É fácil testar os casos de uso sem precisar de banco ou servidor.
Flexibilidade:	Você pode mudar o banco, UI, protocolos sem afetar o núcleo.
Baixo acoplamento:	As partes externas estão plugadas via interfaces, não estão coladas ao domínio.
Manutenção mais simples:	O código está organizado por responsabilidades, não por tecnologia.

📦 Camadas vs Pastas no Projeto

HexagonalSchoolApp.Domain -> Domínio
HexagonalSchoolApp.Application ->	Casos de uso (serviço de aplicação)
HexagonalSchoolApp.Infrastructure ->	Adaptadores (banco de dados, etc.)
HexagonalSchoolApp.API ->	Interface de entrada (HTTP)

exemplo de post

curl -X POST http://localhost:5000/api/student \
     -H "Content-Type: application/json" \
     -d '{"name": "João da Silva", "schoolId": "11111111-1111-1111-1111-111111111111"}'
