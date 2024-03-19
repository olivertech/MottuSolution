Projeto com a proposta de apresentar uma abordagem mais ampla de conhecimentos para montagem de uma solução completa em aspnet core.

Descrição do projeto

A solução desenvolvida segue o conceito conhecido como "Clean Architecture", utilizado em sistemas com arquiteturas modernas, permitindo maior facilidade para manutenção e expansão, e melhor entendimento do código, com divisão clara de responsabilidades para cada uma das camadas que compõem a solução.

A solução está dividida nas seguintes camadas/projetos:

- Projeto Domain e Projeto Application - Contendo as camadas com as regras de negócio. Na atual configuração da solução, não houve necessidade a priore, de implementar a camada Application.
- Projeto Infrastructure - Contem a camada de persistência
- Projeto CrossCutting - Contém a camada com classes referenciadas pelos demais projetos
- Projeto User Interface - Contem a camada de apresentação/Api

O projeto implementa os seguintes patterns:

- Clean Architecture
- n-Tier
- Unit Of Work
- Repository
- Class Mapping
- ORM
 
O projeto utiliza mensageria por meio do RabbitMQ em conjunto com o MassTransit.

Para o banco de dados, foi utilizado o Postgrees versão 16.2, implementando a tecnologia ORM por meio do Entity Framework Core, para abstração do banco de dados. Além disso, por meio do EF Core, foi adotado a abordagem Code First para modelagem de banco de dados.

![image](https://github.com/olivertech/MottuSolution/assets/6912641/65ecc7e8-85a7-4d4e-9ead-7343eca60c3b)


Para dar suporte a execução da solução, é preciso ter instalado o Docker localmente. Pelo Docker, serão criados 2 containers que irão dar suporte na execução do mesmo. Um container do banco de dados e outro do Broker de mensageria RabbitMQ.

Com a presente solução, se propõe mostrar uma meríade de tecnologias sendo utilizadas num projeto aspnet core, de forma a contemplar boas práticas e a adoção de convenções e padrões de mercado, aplicados ao desenvolvimento de sistemas.
