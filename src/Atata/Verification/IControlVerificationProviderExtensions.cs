﻿namespace Atata
{
    public static class IControlVerificationProviderExtensions
    {
        public static TOwner Exist<TControl, TOwner>(this IControlVerificationProvider<TControl, TOwner> should)
            where TControl : IUIComponent<TOwner>
            where TOwner : PageObject<TOwner>
        {
            should.CheckNotNull(nameof(should));

            ATContext.Current.Log.StartVerificationSection($"{should.Control.ComponentFullName} {should.GetShouldText()} exist");

            SearchOptions searchOptions = new SearchOptions
            {
                IsSafely = false,
                Timeout = should.Timeout ?? RetrySettings.Timeout,
                RetryInterval = should.RetryInterval ?? RetrySettings.RetryInterval
            };

            if (should.IsNegation)
                should.Control.Missing(searchOptions);
            else
                should.Control.Exists(searchOptions);

            ATContext.Current.Log.EndSection();

            return should.Owner;
        }

        public static TOwner BeEnabled<TControl, TOwner>(this IControlVerificationProvider<TControl, TOwner> should)
            where TControl : Control<TOwner>
            where TOwner : PageObject<TOwner>
        {
            var dataShould = should.Control.IsEnabled.Should;
            return should.IsNegation ? dataShould.Not.BeTrue() : dataShould.BeTrue();
        }

        public static TOwner BeReadOnly<TData, TControl, TOwner>(this FieldVerificationProvider<TData, TControl, TOwner> should)
            where TControl : EditableField<TData, TOwner>
            where TOwner : PageObject<TOwner>
        {
            return should.Control.IsReadOnly.Should.BeTrue();
        }

        public static TOwner BeReadOnly<TData, TControl, TOwner>(this FieldVerificationProvider<TData, TControl, TOwner>.NegationFieldVerificationProvider should)
            where TControl : EditableField<TData, TOwner>
            where TOwner : PageObject<TOwner>
        {
            return should.Control.IsReadOnly.Should.Not.BeTrue();
        }
    }
}
