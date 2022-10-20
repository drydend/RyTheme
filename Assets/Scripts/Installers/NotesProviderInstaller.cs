using Zenject;

    public class NotesProviderInstaller : MonoInstaller
    {
        private NotesProvider _notesProvider;

        public override void InstallBindings()
        {
            _notesProvider = new NotesProvider();
            
            Container.Bind<NotesProvider>()
                .FromInstance(_notesProvider)
                .AsSingle();
        }
    }