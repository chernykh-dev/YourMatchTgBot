using System.Reflection;

namespace YourMatchTgBot.ReflectionExtensions;

/// <inheritdoc/>
    public class DependencyReflectorFactory : IDependencyReflectorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DependencyReflectorFactory> _logger;

        public DependencyReflectorFactory(IServiceProvider serviceProvider, ILogger<DependencyReflectorFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public T GetReflectedType<T>(Type typeToReflect, object[] constructorRequiredParameters)
            where T : class
        {
            var propertyTypeAssemblyQualifiedName = typeToReflect.AssemblyQualifiedName;
            var constructors = typeToReflect.GetConstructors();
            if (constructors.Length == 0)
            {
                LogConstructorError(typeToReflect, constructorRequiredParameters);
                return null;
            }
            var parameters = GetConstructor(constructors, constructorRequiredParameters)?.GetParameters();
            if (parameters == null)
            {
                LogConstructorError(typeToReflect, constructorRequiredParameters);
                return null;
            }
            object[] injectedParameters = null;
            if (constructorRequiredParameters == null)
            {
                injectedParameters = parameters.Select(parameter => _serviceProvider.GetService(parameter.ParameterType)).ToArray();
            }
            else
            {
                injectedParameters = constructorRequiredParameters
                .Concat(parameters.Skip(constructorRequiredParameters.Length).Select(parameter => _serviceProvider.GetService(parameter.ParameterType)))
                .ToArray();
            }
            
            return (T)Activator.CreateInstance(Type.GetType(propertyTypeAssemblyQualifiedName), injectedParameters);
            // return (T)Activator.CreateInstance(typeToReflect, injectedParameters);
        }

        /// <summary>
        /// Logs a constructor error
        /// </summary>
        /// <param name="typeToReflect"></param>
        /// <param name="constructorRequiredParameters"></param>
        private void LogConstructorError(Type typeToReflect, object[] constructorRequiredParameters)
        {
            string constructorNames = string.Join(", ", constructorRequiredParameters?.Select(item => item.GetType().Name));
            string message = $"Unable to create instance of {typeToReflect.Name}. " +
                $"Could not find a constructor with {constructorNames} as first argument(s)";
            _logger.LogError(message);
        }

        /// <summary>
        /// Takes the required paramters from a constructor
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="constructorRequiredParametersLength"></param>
        /// <returns></returns>
        private ParameterInfo[] TakeConstructorRequiredParamters(ConstructorInfo constructor, int constructorRequiredParametersLength)
        {
            var parameters = constructor.GetParameters();
            if (parameters.Length < constructorRequiredParametersLength)
            {
                return parameters;
            }
            return parameters?.Take(constructorRequiredParametersLength).ToArray();
        }

        /// <summary>
        /// Validates the required parameters from a constructor
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="constructorRequiredParameters"></param>
        /// <returns></returns>
        private bool ValidateConstructorRequiredParameters(ConstructorInfo constructor, object[] constructorRequiredParameters)
        {
            if (constructorRequiredParameters == null)
            {
                return true;
            }
            var parameters = TakeConstructorRequiredParamters(constructor, constructorRequiredParameters.Length);
            for (int i = 0; i < parameters.Length; i++)
            {
                var requiredParameter = constructorRequiredParameters[i].GetType();
                // var requiredParameter = ((ParameterInfo)constructorRequiredParameters[i]).ParameterType;
                if (parameters[i].ParameterType != requiredParameter)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets a constructor
        /// </summary>
        /// <param name="constructors"></param>
        /// <param name="constructorRequiredParameters"></param>
        /// <returns></returns>
        private ConstructorInfo GetConstructor(ConstructorInfo[] constructors, object[] constructorRequiredParameters)
        {
            return constructors?.FirstOrDefault(constructor =>
              ValidateConstructorRequiredParameters(constructor, constructorRequiredParameters));
        }
    }