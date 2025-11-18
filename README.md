# 📱 WpfApp - Sistema de Cadastro

Aplicação desktop desenvolvida em **WPF (Windows Presentation Foundation)** para gerenciamento de Pessoas, Produtos e Pedidos, utilizando arquitetura **MVVM** e persistência em JSON.

## 🎯 Objetivo

Desenvolver uma aplicação desktop robusta e moderna para manipulação de cadastros, com:
- ✅ Persistência de dados em JSON
- ✅ Utilização de LINQ para consultas
- ✅ Interface intuitiva separada em 3 telas principais
- ✅ Aplicação de boas práticas e organização modular
- ✅ Padrão MVVM (Model-View-ViewModel)

## 🛠️ Stack Tecnológica

- **Linguagem:** C#
- **Framework:** .NET Framework 4.6
- **Interface:** WPF (Windows Presentation Foundation)
- **Padrão:** MVVM (Model-View-ViewModel)
- **Persistência:** JSON (Newtonsoft.Json)
- **Consultas:** LINQ

## 📂 Estrutura do Projeto

```
WpfApp/
├── Models/              # Classes de domínio (Pessoa, Produto, Pedido)
├── Views/               # Telas XAML (MainWindow, PessoaView, ProdutoView, PedidoView)
├── ViewModels/          # Lógica MVVM e comandos
├── Services/            # Persistência e regras de negócio
├── Data/                # Arquivos JSON de dados
├── Resources/           # Imagens, ícones e recursos visuais
├── Properties/          # Configurações do projeto
├── App.xaml             # Aplicação WPF e estilos globais
└── README.md            # Este arquivo
```

## 🎨 Funcionalidades

### 👤 Cadastro de Pessoas
- Adicionar, editar e excluir pessoas
- Campos: Nome, CPF, Email, Telefone, Data de Nascimento, Endereço
- Filtro de busca por nome
- Validação de CPF duplicado

### 📦 Cadastro de Produtos
- Gerenciamento completo de produtos
- Campos: Nome, Descrição, Preço, Quantidade em Estoque, Categoria, Código de Barras
- Busca por nome ou categoria
- Controle de estoque
- Alertas de estoque baixo

### 📋 Gerenciamento de Pedidos
- Criação e gerenciamento de pedidos
- Vinculação com clientes e produtos
- Cálculo automático de valores
- Status do pedido (Pendente, Em Andamento, Concluído, Cancelado)
- Controle automático de estoque ao criar/excluir pedidos
- Itens do pedido com subtotais

## 🚀 Como Executar

### Pré-requisitos
- Visual Studio 2017 ou superior
- .NET Framework 4.6 ou superior
- Windows 7 ou superior

### Passo a Passo

1. **Clone o repositório**
   ```bash
   git clone https://github.com/seu-usuario/wpfapp-cadastro.git
   cd wpfapp-cadastro
   ```

2. **Restaure os pacotes NuGet**
   ```bash
   nuget restore
   ```
   Ou pelo Visual Studio: `Ferramentas > Gerenciador de Pacotes NuGet > Restaurar Pacotes`

3. **Compile o projeto**
   - Abra `WpfApp.csproj` no Visual Studio
   - Pressione `Ctrl + Shift + B` ou vá em `Build > Build Solution`

4. **Execute a aplicação**
   - Pressione `F5` ou clique em `Start` no Visual Studio

## 📖 Arquitetura MVVM

O projeto segue o padrão **MVVM (Model-View-ViewModel)** para separação de responsabilidades:

### Models
Classes de domínio que representam as entidades do sistema:
- `Pessoa`: Dados de clientes
- `Produto`: Catálogo de produtos
- `Pedido`: Pedidos e itens

### Views
Interfaces XAML para interação com o usuário:
- `MainWindow.xaml`: Janela principal com navegação
- `PessoaView.xaml`: Tela de cadastro de pessoas
- `ProdutoView.xaml`: Tela de cadastro de produtos
- `PedidoView.xaml`: Tela de gerenciamento de pedidos

### ViewModels
Lógica de apresentação e comandos:
- `MainViewModel`: Navegação entre telas
- `PessoaViewModel`: Lógica do cadastro de pessoas
- `ProdutoViewModel`: Lógica do cadastro de produtos
- `PedidoViewModel`: Lógica de gerenciamento de pedidos

### Services
Camada de persistência e regras de negócio:
- `JsonDataService<T>`: Serviço genérico de persistência
- `PessoaService`: Operações com pessoas
- `ProdutoService`: Operações com produtos
- `PedidoService`: Operações com pedidos

## 💾 Persistência de Dados

Os dados são salvos em arquivos JSON na pasta `Data/`:
- `pessoas.json`: Cadastro de pessoas
- `produtos.json`: Catálogo de produtos
- `pedidos.json`: Histórico de pedidos

**Exemplo de estrutura JSON (Pessoa):**
```json
[
  {
    "Id": "123e4567-e89b-12d3-a456-426614174000",
    "Nome": "João Silva",
    "CPF": "123.456.789-00",
    "Email": "joao@email.com",
    "Telefone": "(11) 98765-4321",
    "DataNascimento": "1990-01-15T00:00:00",
    "Endereco": "Rua das Flores, 123",
    "DataCadastro": "2025-01-01T10:30:00"
  }
]
```

## 🔍 Uso de LINQ

O projeto utiliza **LINQ** extensivamente para consultas:

```csharp
// Busca de pessoas por nome
public List<Pessoa> BuscarPorNome(string nome)
{
    return _dataService.GetAll()
        .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
        .OrderBy(p => p.Nome)
        .ToList();
}

// Produtos com estoque baixo
public List<Produto> ObterProdutosComEstoqueBaixo(int quantidadeMinima = 10)
{
    return _dataService.GetAll()
        .Where(p => p.QuantidadeEstoque <= quantidadeMinima)
        .OrderBy(p => p.QuantidadeEstoque)
        .ToList();
}

// Total de vendas concluídas
public decimal ObterValorTotalVendas()
{
    return _dataService.GetAll()
        .Where(p => p.Status == StatusPedido.Concluido)
        .Sum(p => p.ValorTotal);
}
```

## 🎨 Interface do Usuário

A interface foi desenvolvida com foco em:
- **Usabilidade**: Navegação intuitiva e fluxos claros
- **Design Moderno**: Paleta de cores profissional (#2C3E50, #3498DB)
- **Responsividade**: Layout adaptável
- **Feedback Visual**: Indicadores de estado e ações

### Capturas de Tela
*(Adicione screenshots da aplicação aqui)*

## 📦 Dependências

- **Newtonsoft.Json** (13.0.3): Serialização/Deserialização JSON

## 🧪 Testes

Para testar a aplicação:

1. **Adicione algumas pessoas** na aba "Pessoas"
2. **Cadastre produtos** na aba "Produtos"
3. **Crie pedidos** na aba "Pedidos" vinculando clientes e produtos
4. Observe que o estoque é atualizado automaticamente
5. Os dados são salvos automaticamente no formato JSON

## 📝 Melhorias Futuras

- [ ] Implementar validações mais robustas (CPF, Email)
- [ ] Adicionar relatórios e gráficos
- [ ] Exportar dados para Excel/PDF
- [ ] Implementar sistema de backup automático
- [ ] Adicionar suporte a múltiplos usuários
- [ ] Migrar para WPF .NET 6+ (versão moderna)
- [ ] Adicionar testes unitários
- [ ] Implementar SQLite como alternativa ao JSON

## 👨‍💻 Autor

Desenvolvido com ❤️ seguindo as melhores práticas de desenvolvimento WPF e arquitetura MVVM.

## 📄 Licença

Este projeto é de código aberto e está disponível sob a licença MIT.

---

**Mensagem de Commit Sugerida:**
```
feat: Implementação inicial do WpfApp - Sistema de Cadastro

- Estrutura completa com padrão MVVM
- Cadastro de Pessoas, Produtos e Pedidos
- Persistência em JSON com LINQ
- Interface WPF moderna e intuitiva
- .NET Framework 4.6
```

