*Descrição do projeto*

Projeto com a proposta de apresentar uma abordagem mais ampla de conhecimentos para montagem de uma solução completa em aspnet core.

Esse projeto foi desenvolvido para a empresa Mottu, com objetivo de apresentar uma proposta moderna de arquitetura de sistemas, usando boas práticas para servir de solução para sistemas, que vinha apresentando problemas internos arquiteturais, e de sistemas mais desenvolvidos, sem boas práticas e com dependências fortes (altíssimo acoplamento entre projetos e camadas), causando problemas na manutenção corretiva e até mesmo evolutiva.

A solução desenvolvida aqui segue o conceito arquitetural conhecido como "Clean Architecture", utilizado em sistemas com arquiteturas modernas, permitindo maior facilidade para manutenção e expansão, e melhor entendimento do código, com divisão clara de responsabilidades para cada uma das camadas que compõem a solução.

A solução está dividida nas seguintes camadas/projetos:

- Projeto Domain e Projeto Application - Contém as regras de negócio
- Projeto Infrastructure - Contém a persistência de dados
- Projeto CrossCutting - Contém as classes referenciadas pelos demais projetos
- Projeto User Interface - Contém a apresentação/Api
- Projeto Consumer - Contém a lógica associada ao consumer da mensageria, usado apenas para fins de teste do RabbitMQ

Alguns dos patterns usados no projeto:

- Clean Architecture
- n-Tier
- Unit Of Work
- Repository
- Class Mapping
- ORM
 
O projeto utiliza mensageria por meio do RabbitMQ em conjunto com o MassTransit, rodando por meio de containers.

Para o banco de dados, foi utilizado o Postgrees versão 16.2, implementando a tecnologia ORM com Entity Framework Core, para abstração do banco de dados. Além disso, por meio do EF Core, foi adotado a abordagem "Code First" para modelagem de banco de dados.

A seguir, a modelagem proposta do banco de dados, de acordo com possíveis funcionalidades imaginadas para esse sistema fictício de gestão de locações de moto.

![image](https://github.com/olivertech/MottuSolution/assets/6912641/65ecc7e8-85a7-4d4e-9ead-7343eca60c3b)


Para dar suporte a execução da solução, é preciso ter instalado o Docker localmente. Pelo Docker, serão criados 2 containers que irão dar suporte na execução do mesmo. Um container do banco de dados e outro do Broker de mensageria RabbitMQ.

Com a presente solução, se propõe mostrar uma meríade de tecnologias sendo utilizadas num projeto aspnet core, de forma a contemplar boas práticas e a adoção de convenções e padrões de mercado, aplicados ao desenvolvimento de sistemas, afim de ajudar a solucionar problemas existentes e enfrentados pela empresa Mottu.

Na raiz da solução encontra-se um pdf que abrange mais detalhes do projeto e como instalar e rodar o mesmo localmente.
