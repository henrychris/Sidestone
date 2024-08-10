# VsaTemplate

## Description

This is my template for projects using VSA in C#. It has a consistent folder structure and an editorconfig file is also included.

## Installation

Clone the repository  

```bash
git clone https://github.com/henrychris/VsaTemplate.git
```

cd to the root directory  

```bash
cd VsaTemplate
```

Install the template

```bash
dotnet new install .
```

## Usage

Create a new project

```bash
dotnet new vsatemplate -n YourProjectName
```

You can run the project using the `dotnet run` command in the PlaintSolution.Host folder. The project will be available at `http://localhost:5051`.
Access `http://localhost:5051/swagger` to see the available endpoints.
