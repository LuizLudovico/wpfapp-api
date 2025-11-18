# 🚀 Guia de Configuração do Projeto WpfApp

## 📋 Checklist de Configuração

### 1. Verificar Pré-requisitos
- [ ] Visual Studio 2017 ou superior instalado
- [ ] .NET Framework 4.6 ou superior
- [ ] Git instalado (para controle de versão)

### 2. Configurar o Projeto no Visual Studio

1. Abra o **Visual Studio**
2. Clique em `File > Open > Project/Solution`
3. Selecione o arquivo `WpfApp.csproj`
4. Aguarde a restauração dos pacotes NuGet
5. Compile o projeto (`Ctrl + Shift + B`)
6. Execute (`F5`)

### 3. Restaurar Pacotes NuGet

Se os pacotes não forem restaurados automaticamente:

**Via Visual Studio:**
```
Ferramentas > Gerenciador de Pacotes NuGet > Gerenciar Pacotes NuGet para a Solução
Clique em "Restaurar" no topo da janela
```

**Via Command Line:**
```bash
nuget restore WpfApp.csproj
```

### 4. Inicializar Repositório Git

Execute os seguintes comandos na raiz do projeto:

```bash
# Inicializar repositório
git init

# Adicionar todos os arquivos
git add .

# Primeiro commit
git commit -m "feat: Implementação inicial do WpfApp - Sistema de Cadastro

- Estrutura completa com padrão MVVM
- Cadastro de Pessoas, Produtos e Pedidos
- Persistência em JSON com LINQ
- Interface WPF moderna e intuitiva
- .NET Framework 4.6"

# Criar branch main
git branch -M main

# Adicionar remote (substitua pela URL do seu repositório)
git remote add origin https://github.com/seu-usuario/wpfapp-cadastro.git

# Push inicial
git push -u origin main
```

### 5. Dados de Exemplo

O projeto já vem com dados de exemplo em `Data/`:
- **3 Pessoas** cadastradas
- **5 Produtos** no catálogo
- **3 Pedidos** de exemplo

Você pode:
- Usar esses dados para testes
- Excluir os arquivos JSON para começar do zero
- Modificar conforme necessário

## 🔧 Resolução de Problemas Comuns

### Erro: "Newtonsoft.Json não encontrado"
**Solução:**
```bash
Install-Package Newtonsoft.Json -Version 13.0.3
```

### Erro: "Namespace não encontrado"
**Solução:**
1. Limpe a solução: `Build > Clean Solution`
2. Rebuild: `Build > Rebuild Solution`

### Erro ao executar: "System.IO.FileNotFoundException"
**Solução:**
Verifique se a pasta `Data/` existe na mesma pasta do executável.

### Interface não aparece corretamente
**Solução:**
Verifique se o `App.xaml` está configurado corretamente como `ApplicationDefinition`.

## 📝 Primeiros Passos Após Setup

1. **Explore os Dados de Exemplo**
   - Navegue pelas 3 telas (Pessoas, Produtos, Pedidos)
   - Veja como os dados são carregados do JSON

2. **Teste as Funcionalidades**
   - Adicione uma nova pessoa
   - Cadastre um produto
   - Crie um pedido

3. **Examine o Código**
   - Veja como o MVVM está implementado
   - Analise o uso de LINQ nos Services
   - Entenda a persistência JSON

4. **Customize**
   - Altere cores e estilos no `App.xaml`
   - Adicione novos campos se necessário
   - Implemente validações adicionais

## 🎯 Estrutura de Branches Sugerida

```
main          # Branch principal (produção)
├─ develop    # Branch de desenvolvimento
├─ feature/*  # Novas funcionalidades
├─ bugfix/*   # Correções de bugs
└─ release/*  # Preparação de releases
```

## 📚 Recursos Úteis

- [Documentação WPF](https://docs.microsoft.com/pt-br/dotnet/desktop/wpf/)
- [Padrão MVVM](https://docs.microsoft.com/pt-br/dotnet/architecture/maui/mvvm)
- [LINQ em C#](https://docs.microsoft.com/pt-br/dotnet/csharp/linq/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json/help/html/Introduction.htm)

## ✅ Projeto Configurado!

Se tudo estiver funcionando:
- ✅ Visual Studio compilando sem erros
- ✅ Aplicação executando normalmente
- ✅ Dados sendo salvos em JSON
- ✅ Todas as 3 telas funcionais

**Você está pronto para começar a desenvolver! 🎉**

