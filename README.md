# Introduction 
WebAPI show project, using EF Core. Project uses Net Core 3.0 and is used as Spike to test new features and techniques.
Project uses ORM without repository pattern as POC for ditching repository. Currently it uses service, but additionally it will use MediatR to as POC for simple CQRS.
There are tests created for selected features as example for real projects. Tests are created only for first appearance, so I can check how to write test for something new.

Project uses Onion Architecture

# Project Description
## BlogPost.Core
Contains core of project, domain model, entities and ef migrations. Project is core of application, changes here shouldn't break applicaiton.

## BlogPost.Core.Interfaces
Interfaces for core functionality. Project is core of application, changes here shouldn't break applicaiton.

## BlogPost.Services
Implementation of core functionalities. It's used mostly in WebAPI to process data for users.

## BlogPost.WebApi
Public WebAPI for clients. Simple requests uses dbContext straight in controller, complex request are processed by services.

## BlogPost.WebApi.Types
WebAPI types, like DTO, mappings and validators.

## BlogPost.Laboratory.Tests
Project used to test new features. Project is deatached from project, but I keep it here to have spike stuff in one place, additionally as quick place where I can test confusing example from articles.

# Technologies
Net Core 3.0
EF Core 3.0
Autofac
AutoMapper
FluentValidation
Xunit
FluentAssertions
Moq
