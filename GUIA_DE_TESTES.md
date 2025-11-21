# 🧪 Guia Completo de Testes - WpfApp

## 📋 Índice
1. [Pré-requisitos](#pré-requisitos)
2. [Testes da Tela de Pessoas](#testes-da-tela-de-pessoas)
3. [Testes da Tela de Produtos](#testes-da-tela-de-produtos)
4. [Testes da Tela de Pedidos](#testes-da-tela-de-pedidos)
5. [Testes de Integração](#testes-de-integração)
6. [Checklist Final](#checklist-final)

---

## Pré-requisitos

### 1. Compilar e Executar
```bash
# Abrir a solução no Visual Studio
# Pressionar F5 ou clicar em "Iniciar"
```

### 2. Verificar Estrutura de Dados
- Arquivos JSON devem ser criados em: `Data/pessoas.json`, `Data/produtos.json`, `Data/pedidos.json`
- Se não existirem, serão criados automaticamente na primeira execução

---

## 🧑‍💼 Testes da Tela de Pessoas

### ✅ Teste 1: Filtro por Nome
**Objetivo:** Validar filtro de busca por nome

**Passos:**
1. Clicar na aba "Pessoas"
2. No ComboBox "Filtrar por:", selecionar "Nome"
3. Digitar "João" no campo de busca
4. Verificar que apenas pessoas com "João" no nome aparecem

**Resultado Esperado:** Lista filtrada dinamicamente

---

### ✅ Teste 2: Filtro por CPF
**Objetivo:** Validar filtro de busca por CPF

**Passos:**
1. No ComboBox "Filtrar por:", selecionar "CPF"
2. Digitar "001" no campo de busca
3. Verificar que apenas pessoas com "001" no CPF aparecem

**Resultado Esperado:** Lista filtrada por CPF

---

### ✅ Teste 3: Incluir Nova Pessoa
**Objetivo:** Criar um novo registro

**Passos:**
1. Clicar no botão "➕ Incluir"
2. Verificar que uma nova pessoa "Nova Pessoa" aparece na lista
3. Verificar que o formulário à direita exibe os dados
4. Preencher os campos:
   - **Nome:** João Silva
   - **CPF:** 001.001.001-00 (máscara automática)
   - **Email:** joao@teste.com
   - **Telefone:** (18)99731-6821 (máscara automática)
   - **Data de Nascimento:** 01/01/1990 (máscara automática com /)
   - **Endereço:** Rua Teste, 123

**Resultado Esperado:** 
- Máscaras aplicadas automaticamente
- Campos validados ao perder foco

---

### ✅ Teste 4: Validações de CPF
**Objetivo:** Validar CPF inválido

**Passos:**
1. Incluir nova pessoa
2. Digitar CPF inválido: 111.111.111-11
3. Sair do campo (perder foco)

**Resultado Esperado:** 
- Fundo do campo fica rosa
- Não permite salvar com CPF inválido

---

### ✅ Teste 5: Validações de Email
**Objetivo:** Validar formato de email

**Passos:**
1. Incluir nova pessoa
2. Digitar email inválido: "teste@"
3. Sair do campo

**Resultado Esperado:** 
- Mensagem de erro
- Fundo rosa no campo

---

### ✅ Teste 6: Validações de Data de Nascimento
**Objetivo:** Validar regras de data

**Passos:**
1. Incluir nova pessoa
2. Testar datas inválidas:
   - Data futura: 01/01/2030
   - Menor de 18 anos: 01/01/2020
   - Formato inválido: 99/99/9999

**Resultado Esperado:** 
- Mensagens de erro específicas
- Não permite salvar

---

### ✅ Teste 7: Editar e Salvar
**Objetivo:** Modificar registro existente

**Passos:**
1. Selecionar uma pessoa da lista
2. Clicar no botão "✏️ Editar"
3. Verificar mensagem: "Editando: [Nome]..."
4. Modificar o campo "Nome" para "João Silva Editado"
5. Clicar no botão "💾 Salvar"

**Resultado Esperado:** 
- Mensagem de sucesso
- Formulário limpo automaticamente
- Lista atualizada com novo nome

---

### ✅ Teste 8: Excluir Pessoa
**Objetivo:** Remover registro

**Passos:**
1. Selecionar uma pessoa da lista
2. Clicar no botão "🗑️ Excluir"
3. Confirmar exclusão

**Resultado Esperado:** 
- Diálogo de confirmação
- Pessoa removida da lista
- Formulário limpo

---

### ✅ Teste 9: Incluir Pedido para Pessoa
**Objetivo:** Criar pedido vinculado à pessoa

**Passos:**
1. Selecionar uma pessoa da lista
2. Clicar no botão "📋 Incluir Pedido"
3. Na janela modal:
   - Selecionar produtos
   - Definir quantidades
   - Clicar em "➕ Adicionar"
4. Clicar em "✅ Finalizar Pedido"

**Resultado Esperado:** 
- Janela modal abre
- Produtos adicionados à lista
- Valor total calculado automaticamente
- Pedido salvo e exibido no grid inferior

---

### ✅ Teste 10: Grid de Pedidos da Pessoa
**Objetivo:** Visualizar pedidos vinculados

**Passos:**
1. Selecionar uma pessoa que tenha pedidos
2. Verificar grid "Pedidos da Pessoa"
3. Verificar colunas: Data, Valor, Forma Pgto, Status

**Resultado Esperado:** 
- Lista de pedidos exibida
- Dados corretos

---

### ✅ Teste 11: Filtrar Pedidos da Pessoa
**Objetivo:** Filtrar pedidos por status

**Passos:**
1. Selecionar pessoa com múltiplos pedidos
2. No ComboBox acima do grid, selecionar "Pagos"
3. Verificar que apenas pedidos pagos aparecem
4. Testar outros filtros: Todos, Pendentes, Enviados, Recebidos

**Resultado Esperado:** 
- Filtro aplicado corretamente
- Lista atualizada

---

### ✅ Teste 12: Marcar Status do Pedido
**Objetivo:** Alterar status do pedido

**Passos:**
1. Selecionar pessoa com pedido pendente
2. Selecionar pedido no grid
3. Clicar em "💰 Pago"
4. Verificar que status mudou para "Pago"

**Resultado Esperado:** 
- Status atualizado
- Mensagem de sucesso

---

## 📦 Testes da Tela de Produtos

### ✅ Teste 13: Filtro por Nome
**Objetivo:** Validar filtro de busca por nome

**Passos:**
1. Clicar na aba "Produtos"
2. No ComboBox "Filtrar por:", selecionar "Nome"
3. Digitar "Caneta" no campo de busca

**Resultado Esperado:** 
- Lista filtrada por nome

---

### ✅ Teste 14: Filtro por Código
**Objetivo:** Validar filtro de busca por código

**Passos:**
1. No ComboBox "Filtrar por:", selecionar "Código"
2. Digitar "001" no campo de busca

**Resultado Esperado:** 
- Lista filtrada por código

---

### ✅ Teste 15: Filtro por Faixa de Valor
**Objetivo:** Validar filtro de busca por preço

**Passos:**
1. No ComboBox "Filtrar por:", selecionar "Faixa de Valor"
2. Digitar no campo "Min": 10
3. Digitar no campo "Max": 50
4. Verificar que apenas produtos entre R$ 10 e R$ 50 aparecem

**Resultado Esperado:** 
- Interface muda para exibir campos Min/Max
- Lista filtrada por faixa de preço

---

### ✅ Teste 16: Incluir Novo Produto
**Objetivo:** Criar um novo registro

**Passos:**
1. Clicar no botão "➕ Incluir"
2. Preencher os campos:
   - **Nome:** Caneta Azul
   - **Código:** PEN-001
   - **Descrição:** Caneta esferográfica azul
   - **Preço:** 2,50 (formato decimal com vírgula)
   - **Quantidade em Estoque:** 100
   - **Categoria:** Papelaria
   - **Código de Barras:** 7891234567890

**Resultado Esperado:** 
- Novo produto criado
- Preço aceita formato decimal (2,50)

---

### ✅ Teste 17: Editar e Salvar Produto
**Objetivo:** Modificar registro existente

**Passos:**
1. Selecionar um produto da lista
2. Clicar no botão "✏️ Editar"
3. Modificar o campo "Preço" para 3,99
4. Clicar no botão "💾 Salvar"

**Resultado Esperado:** 
- Mensagem de sucesso
- Formulário limpo
- Preço atualizado na lista

---

### ✅ Teste 18: Excluir Produto
**Objetivo:** Remover registro

**Passos:**
1. Selecionar um produto sem pedidos vinculados
2. Clicar no botão "🗑️ Excluir"
3. Confirmar exclusão

**Resultado Esperado:** 
- Diálogo de confirmação
- Produto removido

---

## 📋 Testes da Tela de Pedidos

### ✅ Teste 19: Novo Pedido - Seleção de Pessoa
**Objetivo:** Validar seleção de cliente

**Passos:**
1. Clicar na aba "Pedidos"
2. Clicar no botão "➕ Novo Pedido"
3. Verificar que janela de seleção abre
4. Selecionar um cliente da lista
5. Clicar em "Selecionar"

**Resultado Esperado:** 
- Janela de seleção abre
- Após selecionar, abre janela de edição de pedido

---

### ✅ Teste 20: Novo Pedido - Adicionar Produtos
**Objetivo:** Adicionar itens ao pedido

**Passos:**
1. Na janela de edição:
   - Selecionar produto no ComboBox
   - Digitar quantidade: 5
   - Clicar em "➕ Adicionar"
2. Repetir para adicionar mais produtos
3. Verificar valor total calculado

**Resultado Esperado:** 
- Produtos adicionados à lista
- Valor total atualizado automaticamente
- Subtotal de cada item correto

---

### ✅ Teste 21: Validação de Estoque
**Objetivo:** Impedir adicionar mais que o disponível

**Passos:**
1. Na janela de edição de pedido
2. Selecionar produto com estoque baixo (ex: 10 unidades)
3. Digitar quantidade: 999
4. Clicar em "Adicionar"

**Resultado Esperado:** 
- Mensagem de erro: "Estoque insuficiente! Disponível: 10"
- Produto não adicionado

---

### ✅ Teste 22: Remover Item do Pedido
**Objetivo:** Excluir produto adicionado

**Passos:**
1. Na janela de edição, adicionar produtos
2. Selecionar um item na lista
3. Clicar em "🗑️ Remover Item"

**Resultado Esperado:** 
- Item removido da lista
- Valor total recalculado

---

### ✅ Teste 23: Finalizar Pedido
**Objetivo:** Salvar pedido com confirmação

**Passos:**
1. Na janela de edição com produtos adicionados
2. Clicar em "✅ Finalizar Pedido"
3. Verificar diálogo de confirmação com valor total
4. Confirmar

**Resultado Esperado:** 
- Diálogo mostra valor total
- Aviso: "Após finalizado, o pedido não poderá ser alterado"
- Pedido salvo e adicionado à lista
- Janela fecha
- Mensagem de sucesso

---

### ✅ Teste 24: Cancelar Pedido
**Objetivo:** Descartar pedido não finalizado

**Passos:**
1. Criar novo pedido e adicionar produtos
2. Clicar em "❌ Cancelar"
3. Confirmar cancelamento

**Resultado Esperado:** 
- Diálogo de confirmação
- Pedido descartado (não salvo)
- Janela fecha

---

### ✅ Teste 25: Filtrar Pedidos por Status
**Objetivo:** Filtrar lista de pedidos

**Passos:**
1. Na tela de pedidos, no ComboBox superior
2. Selecionar "Pendentes"
3. Verificar que apenas pedidos pendentes aparecem
4. Testar outros filtros: Todos, Pagos, Enviados, Recebidos

**Resultado Esperado:** 
- Lista filtrada corretamente

---

### ✅ Teste 26: Marcar Pedido como Pago
**Objetivo:** Alterar status do pedido

**Passos:**
1. Selecionar pedido pendente
2. Clicar em "💰 Marcar Pago"

**Resultado Esperado:** 
- Mensagem de sucesso
- Status alterado para "Pago"
- Lista atualizada conforme filtro

---

### ✅ Teste 27: Marcar como Enviado e Recebido
**Objetivo:** Alterar status sequencialmente

**Passos:**
1. Selecionar pedido pago
2. Clicar em "📦 Marcar Enviado"
3. Verificar status mudou para "Enviado"
4. Clicar em "✅ Marcar Recebido"
5. Verificar status mudou para "Recebido"

**Resultado Esperado:** 
- Status atualizado em cada etapa
- Mensagens de sucesso

---

### ✅ Teste 28: Bloqueio de Edição Após Finalização
**Objetivo:** Impedir modificações em pedidos finalizados

**Passos:**
1. Selecionar pedido com status "Recebido" ou "Enviado"
2. Tentar alterar "Forma de Pagamento"
3. Tentar editar "Observações"

**Resultado Esperado:** 
- ComboBox de Forma de Pagamento **desabilitado**
- Campo Observações **readonly** (cinza)
- Status exibido como texto (não editável)

---

### ✅ Teste 29: Excluir Pedido Pendente
**Objetivo:** Remover pedido não finalizado

**Passos:**
1. Selecionar pedido com status "Pendente"
2. Clicar em "🗑️ Excluir"
3. Confirmar exclusão

**Resultado Esperado:** 
- Diálogo de confirmação
- Pedido removido
- Estoque devolvido

---

### ✅ Teste 30: Tentar Excluir Pedido Finalizado
**Objetivo:** Impedir exclusão de pedido finalizado

**Passos:**
1. Selecionar pedido com status "Recebido"
2. Clicar em "🗑️ Excluir"

**Resultado Esperado:** 
- Apenas pedidos pendentes podem ser excluídos
- Pedidos finalizados não são excluídos

---

## 🔗 Testes de Integração

### ✅ Teste 31: Fluxo Completo de Venda
**Objetivo:** Testar todo o processo de ponta a ponta

**Passos:**
1. **Cadastrar Cliente:**
   - Ir para aba Pessoas
   - Incluir nova pessoa
   - Preencher todos os dados válidos
   - Salvar

2. **Cadastrar Produto:**
   - Ir para aba Produtos
   - Incluir novo produto
   - Definir preço e estoque
   - Salvar

3. **Criar Pedido:**
   - Ir para aba Pedidos
   - Novo Pedido
   - Selecionar cliente criado
   - Adicionar produto criado
   - Finalizar pedido

4. **Verificar na tela de Pessoas:**
   - Voltar para aba Pessoas
   - Selecionar cliente
   - Verificar pedido no grid "Pedidos da Pessoa"

5. **Alterar Status:**
   - Marcar como Pago
   - Marcar como Enviado
   - Marcar como Recebido

**Resultado Esperado:** 
- Fluxo completo funciona
- Dados consistentes em todas as telas
- Pedido vinculado corretamente

---

### ✅ Teste 32: Atualização de Estoque
**Objetivo:** Validar dedução de estoque ao criar pedido

**Passos:**
1. Ir para Produtos e verificar estoque inicial (ex: 100)
2. Criar pedido com 10 unidades do produto
3. Voltar para Produtos e verificar estoque

**Resultado Esperado:** 
- Estoque reduzido para 90

---

### ✅ Teste 33: Devolução de Estoque ao Excluir
**Objetivo:** Validar devolução de estoque

**Passos:**
1. Criar pedido pendente com 5 unidades
2. Verificar estoque (reduzido)
3. Excluir o pedido pendente
4. Verificar estoque novamente

**Resultado Esperado:** 
- Estoque devolvido (aumentou 5 unidades)

---

### ✅ Teste 34: Persistência de Dados
**Objetivo:** Garantir que dados são salvos em JSON

**Passos:**
1. Criar pessoa, produto e pedido
2. Fechar a aplicação (Alt+F4)
3. Abrir a aplicação novamente
4. Verificar se dados continuam lá

**Resultado Esperado:** 
- Dados persistidos corretamente
- Arquivos JSON atualizados: `Data/pessoas.json`, `Data/produtos.json`, `Data/pedidos.json`

---

## ✅ Checklist Final

### Funcionalidades Gerais
- [ ] Aplicação compila sem erros
- [ ] Todas as 3 telas são acessíveis
- [ ] Navegação entre telas funciona
- [ ] Interface é responsiva

### Tela de Pessoas
- [ ] Filtro por Nome funciona
- [ ] Filtro por CPF funciona
- [ ] Máscara de CPF aplica automaticamente
- [ ] Máscara de Telefone aplica automaticamente
- [ ] Máscara de Data de Nascimento aplica automaticamente
- [ ] Validação de CPF funciona
- [ ] Validação de Email funciona
- [ ] Validação de Data funciona (não futuro, 18+ anos)
- [ ] Incluir, Editar, Salvar, Excluir funcionam
- [ ] Salvar limpa formulário automaticamente
- [ ] Incluir Pedido abre modal
- [ ] Grid de Pedidos da Pessoa exibe dados
- [ ] Filtros de pedidos funcionam
- [ ] Ações de status (Pago, Enviado, Recebido) funcionam

### Tela de Produtos
- [ ] Filtro por Nome funciona
- [ ] Filtro por Código funciona
- [ ] Filtro por Faixa de Valor funciona
- [ ] Campos dinâmicos conforme filtro
- [ ] Preço aceita formato decimal (12,50)
- [ ] Incluir, Editar, Salvar, Excluir funcionam
- [ ] Salvar limpa formulário automaticamente

### Tela de Pedidos
- [ ] Novo Pedido abre seleção de pessoa
- [ ] Modal de seleção exibe lista de clientes
- [ ] Após selecionar, abre modal de edição
- [ ] Adicionar produtos funciona
- [ ] Validação de estoque funciona
- [ ] Remover item funciona
- [ ] Cálculo de valor total automático
- [ ] Finalizar Pedido salva e fecha modal
- [ ] Cancelar descarta pedido
- [ ] Filtro por status funciona
- [ ] Marcar Pago, Enviado, Recebido funcionam
- [ ] Excluir apenas pedidos pendentes
- [ ] Campos bloqueados após finalização

### Integração
- [ ] Criar pessoa → Pedido vinculado aparece
- [ ] Criar pedido → Estoque reduzido
- [ ] Excluir pedido → Estoque devolvido
- [ ] Dados persistem após fechar aplicação
- [ ] JSON files criados e atualizados

---

## 🐛 Reporte de Bugs

Se encontrar algum problema durante os testes, documente:

**Template de Bug:**
```
ID: BUG-001
Tela: [Pessoas/Produtos/Pedidos]
Teste: [Número do teste]
Descrição: [O que aconteceu]
Esperado: [O que deveria acontecer]
Passos para Reproduzir:
1. 
2. 
3. 
```

---

## ✅ Conclusão

Após executar todos os 34 testes e validar o checklist, a aplicação estará **100% testada** conforme a especificação técnica.

**Status dos Testes:** ⬜ Não Iniciado | 🔄 Em Progresso | ✅ Completo

