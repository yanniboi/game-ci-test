using System;
using System.Collections.Generic;
using NUnit.Framework;
using UI;

namespace Tests.UI
{
    public class PlayerInteractionTests
    {

        public class TestInteractables
        {
            [Test]
            public void _0_interactables_does_not_start_interaction()
            {
                Interaction interaction = new Interaction();
                interaction.Interact();
                Assert.AreEqual(false, interaction.IsInteracting);
            }
        
            [Test]
            public void _1_interactable_starts_interaction()
            {
                Interactable interactable = new Interactable();
                Interaction interaction = new Interaction(interactable);
                interaction.Interact();
                Assert.AreEqual(true, interaction.IsInteracting);
            }
        
            [Test]
            public void _2_interactables_does_not_start_interaction()
            {
                List<Interactable> interactables = new List<Interactable>();

                for (int i = 0; i < 2; i++)
                {
                    Interactable interactable = new Interactable();
                    interactables.Add(interactable);
                }
            
                Interaction interaction = new Interaction(interactables);
                interaction.Interact();
                Assert.AreEqual(false, interaction.IsInteracting);
            }
        }
        
        public class TestCalloutInteractions
        {

            [Test]
            public void callout_is_not_showing_by_default()
            {
                Callout callout = new Callout();
                Assert.AreEqual(false, callout.IsShowing);
            }
            
            [Test]
            public void callout_showing_after_interaction_with_1_npc()
            {
                Identity identity = new Identity();
                Callout callout = new Callout(identity);
                callout.SetInteraction(identity);
                identity.Interact();
                Assert.AreEqual(true, callout.IsShowing);
            }

            [Test]
            public void callout_showings_no_name_before_interaction()
            {
                Identity identity = new Identity();
                Callout callout = new Callout(identity);
                Assert.Null(callout.Name);
                Assert.Null(callout.Text);
            }
            
            [Test]
            public void callout_showings_no_identity_data_if_not_set_on_identity()
            {
                Identity identity = new Identity();
                Callout callout = new Callout(identity);
                callout.SetInteraction(identity);
                identity.Interact();
                Assert.Null(callout.Name);
                Assert.Null(callout.Text);
            }
            
            [Test]
            public void callout_showings_identity_data_after_interaction()
            {
                string name = "Steve";
                string text = "Something to say";
                
                Identity identity = new Identity(name, text);
                Callout callout = new Callout(identity);
                callout.SetInteraction(identity);
                identity.Interact();
                Assert.AreEqual(name, callout.Name);
                Assert.AreEqual(text, callout.Text);
            }
        }

        public partial class TestInteractionSelection
        {

            [Test]
            public void selection_with_single_entry_is_highlighted()
            {
                Selectable firstSelectable = new Selectable();
                Assert.AreEqual(false, firstSelectable.IsHighlighted);
                Selection selection = new Selection(firstSelectable);
                Assert.AreEqual(true, firstSelectable.IsHighlighted);
            }
            
            [Test]
            public void selection_with_multiple_entries_first_is_highlighted()
            {
                Selectable firstSelectable = new Selectable();
                Assert.AreEqual(false, firstSelectable.IsHighlighted);

                Selectable secondSelectable = new Selectable();
                Assert.AreEqual(false, secondSelectable.IsHighlighted);

                new Selection(new ISelectable[] {firstSelectable, secondSelectable});
                Assert.AreEqual(true, firstSelectable.IsHighlighted);
                Assert.AreEqual(false, secondSelectable.IsHighlighted);
            }
            
            [Test]
            public void selection_with_single_entry_first_is_highlighted_after_scroll()
            {
                Selectable firstSelectable = new Selectable();
                Selection selection = new Selection(firstSelectable);
                selection.Scroll();
                Assert.AreEqual(true, firstSelectable.IsHighlighted);
            }
            
            [Test]
            public void selection_with_multiple_entries_second_is_highlighted_after_scroll()
            {
                Selectable firstSelectable = new Selectable();
                Selectable secondSelectable = new Selectable();
                Selection selection = new Selection(new ISelectable[] {firstSelectable, secondSelectable});
                selection.Scroll();

                Assert.AreEqual(false, firstSelectable.IsHighlighted);
                Assert.AreEqual(true, secondSelectable.IsHighlighted);
            }
            
            [Test]
            public void selectable_is_not_highlighted_after_selection_is_made()
            {
                Selectable firstSelectable = new Selectable();
                Selection selection = new Selection(firstSelectable);
                selection.MakeSelection();
                Assert.AreEqual(false, firstSelectable.IsHighlighted);
            }
        }
    }
}
