# 🎨 Recursos Visuais - WpfApp

Esta pasta contém todos os recursos visuais da aplicação.

## 📂 Estrutura

```
Resources/
├── Icons/
│   ├── app-icon.svg          (Ícone da aplicação - 256×256)
│   ├── icon-add.svg          (Botão Adicionar - 24×24)
│   ├── icon-edit.svg         (Botão Editar - 24×24)
│   ├── icon-save.svg         (Botão Salvar - 24×24)
│   ├── icon-delete.svg       (Botão Excluir - 24×24)
│   └── icon-search.svg       (Botão Buscar - 24×24)
├── Images/
│   ├── logo.svg              (Logo da aplicação - 400×100)
│   └── empty-state.svg       (Estado vazio - 300×300)
└── README.md                 (este arquivo)
```

---

## 🎨 Paleta de Cores

Todos os recursos seguem a paleta de cores da aplicação:

| Cor | Hex | Uso |
|-----|-----|-----|
| Azul Primário | `#3498DB` | Logo, botões principais |
| Cinza Escuro | `#2C3E50` | Background, textos |
| Verde Sucesso | `#27AE60` | Botões de ação positiva |
| Vermelho Erro | `#E74C3C` | Botões de exclusão |
| Cinza Claro | `#ECF0F1` | Backgrounds secundários |
| Cinza Médio | `#BDC3C7` | Bordas, separadores |

---

## ⚠️ IMPORTANTE: Conversão Necessária

Os arquivos estão em **SVG** (formato vetorial). O WPF não suporta SVG nativamente, então é necessário converter para **PNG** e **ICO**.

**Erro comum:**
```
IOException: Não é possível localizar o recurso 'resources/icons/app-icon.ico'
```

**Solução:** Converta os arquivos SVG conforme instruções abaixo.

---

## 🔄 Como Converter SVG para PNG/ICO

### Opção 1: Online (Rápido) ⭐ RECOMENDADO

**Para PNG:**
1. Acesse: https://svgtopng.com/
2. Faça upload do arquivo SVG
3. Escolha o tamanho desejado
4. Download PNG

**Para ICO (Ícone da aplicação):**
1. Converta `app-icon.svg` para PNG (256×256)
2. Acesse: https://convertico.com/
3. Upload do PNG
4. Download do arquivo `.ico`
5. Renomeie para `app-icon.ico`

### Opção 2: Inkscape (Profissional)

```bash
# Instalar Inkscape: https://inkscape.org/

# Converter SVG para PNG
inkscape app-icon.svg --export-png=app-icon.png --export-width=256

# Para ICO, use online: https://convertico.com/
```

### Opção 3: ImageMagick (Linha de Comando)

```bash
# Instalar ImageMagick: https://imagemagick.org/

# Converter SVG para PNG
magick app-icon.svg -resize 256x256 app-icon.png

# Criar ICO com múltiplos tamanhos
magick app-icon.svg -define icon:auto-resize=256,128,64,48,32,16 app-icon.ico
```

---

## 🚀 Arquivos Necessários para Produção

Após converter, você precisará ter:

```
Resources/
├── Icons/
│   ├── app-icon.ico          ← CONVERTER!
│   ├── icon-add.png          ← CONVERTER (opcional)
│   ├── icon-edit.png         ← CONVERTER (opcional)
│   ├── icon-save.png         ← CONVERTER (opcional)
│   ├── icon-delete.png       ← CONVERTER (opcional)
│   └── icon-search.png       ← CONVERTER (opcional)
└── Images/
    ├── logo.png              ← CONVERTER!
    └── empty-state.png       ← CONVERTER (opcional)
```

**Prioridade:**
1. ✅ **app-icon.ico** (OBRIGATÓRIO)
2. ✅ **logo.png** (RECOMENDADO)
3. ⚠️ Ícones de botões (OPCIONAL - já usamos emojis)

---

## 📐 Tamanhos Recomendados

| Recurso | Tamanho | Formato |
|---------|---------|---------|
| app-icon.ico | 256×256 (multi-size) | ICO |
| logo.png | 400×100 | PNG |
| empty-state.png | 300×300 | PNG |
| icon-*.png | 24×24 | PNG |

---

## 🔗 Como Usar no Código

### 1. Ícone da Aplicação

**MainWindow.xaml:**
```xml
<Window Icon="/Resources/Icons/app-icon.ico">
```

**WpfApp.csproj:**
```xml
<ApplicationIcon>Resources\Icons\app-icon.ico</ApplicationIcon>
```

### 2. Logo no Topo

**MainWindow.xaml:**
```xml
<Image Source="/Resources/Images/logo.png" 
       Height="50" 
       Margin="10,5"
       HorizontalAlignment="Left"/>
```

### 3. Estado Vazio

**Em qualquer View:**
```xml
<Image Source="/Resources/Images/empty-state.png"
       Width="200"
       Visibility="{Binding HasData, Converter={StaticResource BoolToVisibilityConverter}}"/>
```

---

## 🎨 Personalização

Se quiser personalizar as cores:

1. Abra o arquivo SVG em um editor de texto
2. Procure por `fill="#3498DB"` (ou outra cor)
3. Substitua pela cor desejada
4. Salve e reconverta para PNG/ICO

**Exemplo:**
```xml
<!-- Antes -->
<circle cx="128" cy="128" r="120" fill="#3498DB"/>

<!-- Depois (cor roxa) -->
<circle cx="128" cy="128" r="120" fill="#9B59B6"/>
```

---

## 📦 Build Actions no Visual Studio

No `WpfApp.csproj`, os recursos devem estar configurados como:

```xml
<Resource Include="Resources\Icons\app-icon.ico" />
<Resource Include="Resources\Images\logo.png" />
<Resource Include="Resources\Images\empty-state.png" />
```

Ou no Visual Studio:
1. Clique com botão direito no arquivo
2. **Properties**
3. **Build Action** → **Resource**
4. **Copy to Output Directory** → **Do not copy**

---

## 🌟 Recursos Adicionais

### Fontes Gratuitas de Ícones:
- **Flaticon**: https://www.flaticon.com/
- **Icons8**: https://icons8.com/
- **Material Icons**: https://fonts.google.com/icons

### Ferramentas de Design:
- **Canva**: https://www.canva.com/ (logos)
- **Figma**: https://www.figma.com/ (design profissional)
- **Inkscape**: https://inkscape.org/ (editor SVG gratuito)

---

## ✅ Checklist de Conversão

Após converter os SVGs, marque os arquivos criados:

- [ ] `app-icon.ico` (256×256, multi-size)
- [ ] `logo.png` (400×100, transparente)
- [ ] `empty-state.png` (300×300, transparente)
- [ ] Ícones de botões (opcional)

---

## 🎯 Status Atual

**Arquivos Criados:**
- ✅ 6 ícones SVG (botões)
- ✅ 1 ícone SVG da aplicação
- ✅ 1 logo SVG
- ✅ 1 imagem de estado vazio SVG

**Próximos Passos:**
1. **Converter** app-icon.svg → app-icon.ico
2. **Converter** logo.svg → logo.png
3. **Atualizar** MainWindow.xaml com logo
4. **Configurar** ícone da aplicação

---

**🎨 Recursos prontos para conversão e uso!**

