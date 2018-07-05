(function () {
    // Private function
    function getColumnsForScaffolding(data) {
        if ((typeof data.length !== 'number') || data.length === 0) {
            return [];
        }
        var columns = [];
        for (var propertyName in data[0]) {
            columns.push({ headerText: propertyName, rowText: propertyName });
        }
        return columns;
    }

    ko.simpleGrid = {
        // Defines a view model class you can use to populate a grid
        viewModel: function (configuration) {
            _self = this;
            _self.data = configuration.data;
            _self.currentPageIndex = ko.observable(0);
            _self.pageSize = configuration.pageSize || 5;
            _self.newRecords = configuration.newRecords;

            var pagesToShowDefault = 10;
            if (configuration.pagesToShow > 1)
                _self.pagesToShow = configuration.pagesToShow || pagesToShowDefault;
            else
                _self.pagesToShow = pagesToShowDefault;

            _self.maxPageIndex = function () {
                return Math.ceil(ko.unwrap(this.data).length / this.pageSize) - 1;
            };

            /*Indica si hay mas paginas a mostrar*/
            _self.morePages = ko.observable(false);

            _self.pageInit = ko.observable(0);
            _self.pageEnd = ko.observable(0);
            if (ko.unwrap(_self.data).length > 0) {
                _self.pageEnd = ko.unwrap(_self.pageInit) + Math.min(_self.pagesToShow - 1, _self.maxPageIndex());
                _self.morePages = _self.pageEnd < _self.maxPageIndex(); //Chequea si hay mas paginas para mostrar
                _self.pagesArray = ko.observableArray(ko.utils.range(_self.pageInit, _self.pageEnd));
            }
            else
                _self.pagesArray = ko.observableArray();


            _self.pagesTotal = ko.computed(function () {
                return Math.ceil(ko.unwrap(this.data).length / this.pageSize);
            }, this);


            // If you don't specify columns configuration, we'll use scaffolding
            _self.columns = configuration.columns || getColumnsForScaffolding(ko.unwrap(_self.data));

            _self.itemsOnCurrentPage = ko.computed(function () {
                var startIndex = _self.pageSize * _self.currentPageIndex();
                return _self.data.slice(startIndex, startIndex + _self.pageSize);
            }, this);

            _self.drawPages = ko.computed(function () {
                var rowsCount = ko.unwrap(_self.newRecords);

                if (rowsCount <= 0) {
                    //Significa que esta recuperando registros
                    _self.currentPageIndex(0);
                    removePageArrays();
                }
                else {
                    if (_self.currentPageIndex() < _self.pagesToShow) {
                        _self.pageInit = 0; //Inicializa el array de paginas y marcas
                        //Aplica el min por si hay menos registros que el total de paginas a mostrar
                        _self.pageEnd = Math.min(_self.pagesToShow - 1, _self.maxPageIndex());
                    }

                    generatePagesArray(_self.pageInit, _self.pageEnd);
                }

            }, this);

            /*Evento que se dispara al avanzar de pagina*/
            _self.nextPage = function () {
                //Chequea si hay mas paginas para mostrar
                _self.morePages = _self.currentPageIndex() < _self.maxPageIndex();

                if (!_self.morePages) return;

                var nextPageIndex = _self.currentPageIndex() + 1;

                if (nextPageIndex > _self.pageEnd) performPagingFwd();

                _self.currentPageIndex(nextPageIndex);
            };

            /*Se mueve a la pagina anterior*/
            _self.prevPage = function () {
                var pageIndex = _self.currentPageIndex();

                if (pageIndex == 0) return;

                var prevPage = pageIndex - 1;

                if (prevPage < _self.pageInit) performPagingBack();

                _self.currentPageIndex(prevPage);
            };

            /*Muestra la ultima pagina*/
            _self.gotoEnd = function () {
                _self.morePages = false;
                var pageIndexReal = _self.maxPageIndex();
                var pageInitCurrent = _self.pagesToShow * Math.floor(pageIndexReal / _self.pagesToShow);

                _self.pageInit = pageInitCurrent;
                _self.pageEnd = _self.maxPageIndex();

                _self.currentPageIndex(_self.maxPageIndex());
            };

            /*Muestra la primer pagina*/
            _self.gotoBegin = function () {
                _self.morePages = true;

                _self.pageInit = 0;
                _self.pageEnd = Math.min(_self.maxPageIndex(), _self.pagesToShow - 1);
                _self.currentPageIndex(0);
            };

            /************************/
            /* FUNCIONES AUXILIARES */
            /************************/
            function performPagingBack() {
                var pageIndexReal = _self.currentPageIndex() + 1;
                var pageInitCurrent = _self.pagesToShow * Math.floor(pageIndexReal / _self.pagesToShow);

                _self.pageInit = Math.max(0, pageInitCurrent - _self.pagesToShow);
                _self.pageEnd = pageInitCurrent - 1;
            };

            /*Arma el pagina hacia adelante*/
            function performPagingFwd() {
                var pageIndexReal = _self.currentPageIndex() + 1;
                var pageEndCurrent = _self.pagesToShow * Math.ceil(pageIndexReal / _self.pagesToShow);

                pageEndCurrent = Math.min(_self.maxPageIndex(), pageEndCurrent);

                _self.pageInit = pageEndCurrent;
                _self.pageEnd = Math.min(_self.maxPageIndex(), pageEndCurrent + _self.pagesToShow - 1);
            }

            function removePageArrays() {
                _self.pagesArray.removeAll();
            }

            function generatePagesArray(beginIndex, endIndex) {
                removePageArrays();
                var arrNerPages = ko.utils.range(beginIndex, endIndex);
                for (var i = 0; i < arrNerPages.length; i++) {
                    _self.pagesArray.push(arrNerPages[i]);
                };
            }

        } // Fin viewModel: function (configuration) 
    }; // Fin ko.simpleGrid

    // Templates used to render the grid
    var templateEngine = new ko.nativeTemplateEngine();

    templateEngine.addTemplate = function (templateName, templateMarkup) {
        document.write("<script type='text/html' id='" + templateName + "'>" + templateMarkup + "<" + "/script>");
    };

    templateEngine.addTemplate("ko_simpleGrid_grid", "\
                    <table style=\"table-layout:fixed\" class=\"ko-grid table table-striped\" cellspacing=\"0\">\
                            <thead>\
                                <tr data-bind=\"foreach: columns\">\
                                        <!-- ko if: $data.isHtml -->\
                                                <!-- ko if: $data.setWitdh -->\
                                                    <th data-bind=\"html: typeof headerText == 'function' ? headerText() : headerText ,  style:{ width : rowWitdh } \"></th>\
                                                <!-- /ko-->\
                                                <!-- ko ifnot: $data.setWitdh -->\
                                                    <th data-bind=\"html: typeof headerText == 'function' ? headerText() : headerText\"></th>\
                                                <!-- /ko-->\
						                <!-- /ko -->\
						                <!-- ko ifnot: $data.isHtml -->\
                                            <!-- ko if: $data.setWitdh -->\
                                                    <th data-bind=\"text: typeof headerText == 'function' ? headerText() : headerText ,  style:{ width : rowWitdh } \"></th>\
                                                <!-- /ko-->\
                                                <!-- ko ifnot: $data.setWitdh -->\
                                                    <th data-bind=\"text: typeof headerText == 'function' ? headerText() : headerText\"></th>\
                                                <!-- /ko-->\
                                        <!-- /ko -->\
                                   </tr>\
                            </thead>\
                        <tbody data-bind=\"foreach: itemsOnCurrentPage\">\
                            <tr data-bind=\"foreach: $parent.columns\">\
						        <!-- ko if: $data.isHtml -->\
								    <td data-bind=\"html: typeof rowText == 'function' ? rowText($parent) : $parent[rowText]\" style=\"overflow-wrap: break-word; word-wrap: break-word\"></td>\
						        <!-- /ko -->\
						        <!-- ko ifnot: $data.isHtml -->\
								    <td data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText]\" style=\"overflow-wrap: break-word; word-wrap: break-word\"></td>\
						        <!-- /ko -->\
                            </tr>\
                        </tbody>\
                        </table>");
    templateEngine.addTemplate("ko_simpleGrid_pageLinks", "\
                    <div class=\"row\" style=\"text-align:center;\">\
                        <div class=\"col-md-12\" style=\"margin:15px 0px;\">\
                            <span></span>\
                            <a href=\"#\" class=\"btn green\" data-bind=\"click: function() { $root.gotoBegin() }\">\
                                <<\
                            </a>\
                            <a href=\"#\" class=\"btn green\" data-bind=\"click: function() { $root.prevPage() }\">\
                                <\
                            </a>\
                            <!-- ko foreach: pagesArray -->\
                                   <a href=\"#\" class=\"btn green\" data-bind=\"text: $data + 1, click: function() { $root.currentPageIndex($data) }, css: { selected: $data == $root.currentPageIndex() }\">\
                                </a>\
                            <!-- /ko -->\
                            <a href=\"#\" class=\"btn green\" data-bind=\"click: function() { $root.nextPage() }\">\
                                >\
                            </a>\
                            <a href=\"#\" class=\"btn green\" data-bind=\"click: function() { $root.gotoEnd() }\">\
                                >>\
                            </a>\
                            <div class=\"alert-info\" style=\"margin-top:5px\" data-bind =\"visible: pagesArray().length > 0\">\
                                <small>Total de registros: </small><small data-bind=\"text: $root.data().length\"></small> - \
                                <small>Total de p&aacute;ginas: </small><small data-bind=\"text: pagesTotal\"></small>\
                            </div>\
                        </div>\
                    </div>");

    // The "simpleGrid" binding
    ko.bindingHandlers.simpleGrid = {
        init: function () {
            return { 'controlsDescendantBindings': true };
        },
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function (element, viewModelAccessor, allBindings) {
            var viewModel = viewModelAccessor();

            // Empty the element
            while (element.firstChild)
                ko.removeNode(element.firstChild);

            // Allow the default templates to be overridden
            var gridTemplateName = allBindings.get('simpleGridTemplate') || "ko_simpleGrid_grid",
                pageLinksTemplateName = allBindings.get('simpleGridPagerTemplate') || "ko_simpleGrid_pageLinks";

            // Render the main grid
            var gridContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(gridTemplateName, viewModel, { templateEngine: templateEngine }, gridContainer, "replaceNode");

            // Render the page links
            var pageLinksContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(pageLinksTemplateName, viewModel, { templateEngine: templateEngine }, pageLinksContainer, "replaceNode");
        }
    };
})();