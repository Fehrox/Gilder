using Fluxor;
using Portal.Store.Modals;

namespace Presentation.Store.Modals;

public static class ModalDisplayActionExtensions {
    public static ModalAction WithParams(this ModalAction obj, params (string Param, object Value)[] values)
    {
        foreach (var (param, value) in values)
            if (obj.Parameters != null)
                obj.Parameters[param] = value;

        return obj;
    }
}

public record ModalAction
{
    public Type ModalType { get; init; }
    public Dictionary<string, object>? Parameters { get; init; }
    
    public ModalAction(Type modalType, Dictionary<string, object>? parameters = null)
    {
        ModalType = modalType;
        Parameters = parameters ?? new Dictionary<string, object>();
    }

    public static Dictionary<string, object> MapPropertiesFrom(object obj)
    {
        var properties = obj.GetType().GetProperties();
        var result = new Dictionary<string, object>();
        foreach (var property in properties) {
            var value = property.GetValue(obj);
            if(value == null) continue;
            
            result[property.Name] = value;
        }
        return result;
    }
}

public static class DictionaryExtensions
{
    public static Dictionary<string, object> WithProperties(
        this Dictionary<string, object> dict,
        params (string Param, object Value)[] values)
    {

        foreach (var (param, value) in values)
            dict[param] = value;

        return dict;
    }
}

public class ModalDisplayActionReducer : Reducer<ModalState, ModalAction>
{
    public override ModalState Reduce(ModalState state, ModalAction action)
    {
        
        var t = action.ModalType;
        var parameters = action.Parameters ?? new Dictionary<string, object>();
        state.RenderingModals.Add(Guid.NewGuid(), new KeyValuePair<Type, Dictionary<string, object>>(t, parameters));

        return state with {
            RenderingModals = new Dictionary<Guid, KeyValuePair<Type, Dictionary<string, object>>>(state.RenderingModals)
        };
    }
}

public class ModalDisplayException : Exception
{
    public ModalDisplayException(Type type) : base(type.Name + " must be of type ModalComponent") { }
}