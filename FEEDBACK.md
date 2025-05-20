# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC bem estruturado com controllers e views funcionais para Produto e Categoria.
    - Views organizadas por área, como `Views/Product/` e `Views/Category/`, com suporte completo a CRUD.
    - Uso de `Areas/Identity` para autenticação, mantendo isolamento e clareza da navegação.

### Design
  - Views padronizadas, uso de layout compartilhado (`_Layout.cshtml`) e componentes (`_TempDataAlerts`, `CategoryDropdown`) indicam boa organização e reaproveitamento.
  - Foco no funcional mais do que no visual, mas entrega uma UX adequada ao escopo.

### Funcionalidade
  * Pontos positivos:
    - CRUD completo para Produtos e Categorias via views.
    - Actions no MVC interagem com serviços, persistem dados e retornam feedback ao usuário.
    - Componentes como filtro de categorias e dropdown indicam preocupação com a experiência de uso.  

## Back End

### Arquitetura
  * Pontos positivos:
    - Projeto segue arquitetura enxuta e coesa conforme o escopo: apenas API, MVC e Core.
    - Estrutura clara entre projetos (`.App`, `.Api`), separando responsabilidades.
    - Controllers, serviços e validações bem organizados.

  * Pontos negativos:
    - Nenhum exagero identificado. A arquitetura está precisa para o tamanho do projeto.

### Funcionalidade
  * Pontos positivos:
    - API REST implementada com endpoints protegidos via JWT.
    - CRUD funcional para Produto e Categoria na API.
    - Seller é criado corretamente no momento do registro do usuário via MVC (`Register.cshtml.cs`, linha 124).
    - Seed de dados e migrations automáticas implementadas corretamente via `DatabaseExtension.EnsureSeedData()`.

### Modelagem
  * Pontos positivos:
    - Entidades simples e coerentes com o domínio (Produto, Categoria, Seller).
    - Uso de `ViewModel` para comunicação entre camadas no MVC.
    - Boa separação entre models de domínio e modelos de entrada/saída.
  
  * Pontos negativos:
    - O uso do inglês deve ser feito quando utilizado, estamos criando uma aplicação com uma linquagem definida em português `Vendedor`, `Produto`, etc...

## Projeto

### Organização
  * Pontos positivos:
    - Uso da pasta `src/` com subprojetos claramente nomeados.
    - Solução `.sln` presente na raiz.
    - Separação clara entre API e MVC, com nomeação padronizada.

### Documentação
  * Pontos positivos:
    - README.md presente.
    - Swagger implementado com autenticação JWT.

  * Pontos negativos:
    - README sem instruções técnicas de setup e execução completas.
    - `FEEDBACK.md` presente, mas vazio.

### Instalação
  * Pontos positivos:
    - Projeto compilável e executável a partir do `.sln`.
    - SQLite implementado para ambiente de desenvolvimento.
    - Migrations automáticas e seed de dados presentes e configuradas.
---

# 📊 Matriz de Avaliação de Projetos

| Critério                         | Peso | Nota (0-10) | Justificativa                                                                 |
|----------------------------------|------|-------------|------------------------------------------------------------------------------|
| **Funcionalidade**              | 30%  | 10          | CRUD completo front/back, Seller criado, seed e SQLite presentes.           |
| **Qualidade do Código**         | 20%  | 10           | Código limpo, modular, boas práticas.                                        |
| **Eficiência e Desempenho**     | 20%  | 10           | Projeto leve, modular, com seed automático e estrutura coesa.               |
| **Inovação e Diferenciais**     | 10%  | 10           | Uso de partials, componentes, boas práticas no MVC.                         |
| **Documentação e Organização**  | 10%  | 8           | Estrutura sólida, documentação básica, deveria ser em portugues                   |
| **Resolução de Feedbacks**      | 10%  | 10           | Tudo ok.                      |

---

## 🎯 **Nota Final: 9.8 / 10**
